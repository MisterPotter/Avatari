using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/**
* @author: Charlotte
* @summary: Stores the player statistics
*/
[Serializable]
public class PlayerStatistic : IEnumerable<Statistic> {
    public Level level; //
    public Experience experience; //
    public Health health;
    public Strength strength; //
    public Agility agility; //
    public Defense defense; // 

    public readonly uint Count = 6;

    public PlayerStatistic() {
        level = new Level();
        experience = new Experience();
        health = new Health();
        strength = new Strength();
        agility = new Agility();
        defense = new Defense();
    }

    public PlayerStatistic(int level, int experience, int health, int strength, int agility, int defense) {
        this.level = new Level(level);
        this.experience = new Experience(experience);
        this.health = new Health(health);
        this.strength = new Strength(strength);
        this.agility = new Agility(agility);
        this.defense = new Defense(defense);
    }

    public IEnumerator<Statistic> GetEnumerator() {
        yield return level;
        yield return experience;
        yield return health;
        yield return strength;
        yield return agility;
        yield return defense;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    /**
     * Called once the app is opened for the first time in a day
     * Need to be passed an instance of the cache to access the fitbit data,
     * as well as the date to update from
     */
    public void UpdatePlayerStatisticsSince(Cache cache, DateTime date) {
        updateExperience(cache, date);
        updateHealth(cache, date);
        updateStrength(cache, date);
        updateAgility(cache, date);
        updateDefense(cache, date);
        UpdateLevel();
    }

    /**
     * Update the player's statistic upon item equip or unequip.
     */ 
    public void UpdateStatOnItemChange(Item item, bool equipped) {
        int value = item.statBoost;
        if (!equipped) {
            value = -value;
        }
        switch(item.statType) {
            case Statistic.Type.Experience:
                this.experience.CurrentValue += value;
                break;
            case Statistic.Type.Health:
                this.health.CurrentValue += value;
                break;
            case Statistic.Type.Agility:
                this.agility.CurrentValue += value;
                break;
            case Statistic.Type.Defense:
                this.defense.CurrentValue += value;
                break;
            case Statistic.Type.Strength:
                this.strength.CurrentValue += value;
                break;
            case Statistic.Type.Level:
                this.level.CurrentValue += value;
                break;
            default:
                throw new Exception("Invalid Statistic type on item equip or unequip: " + item.statType);
        }
    }

    /**
     * Check the experience level and determine if level needs to be increased
     */
    public void UpdateLevel() {

        // If we're already at highest level, do nothing
        if (level.CurrentValue == level.maxValue) {
            return;
        }

        int expToCurrLevel = Constants.ExperienceToLevel[level.CurrentValue];

        // Sanity check
        Debug.Assert(experience.CurrentValue < expToCurrLevel, "Current experience is less than current level. Experience should only ever increase.");

        int nextLevel = level.CurrentValue + 1;
        int expToNextLevel = Constants.ExperienceToLevel[nextLevel];

        // don't have enough experience to go up a level, do nothing
        if (experience.CurrentValue < expToNextLevel) {
            return;
        } else {
            while( experience.CurrentValue >= expToNextLevel) {
                level.CurrentValue += 1;
                // if this made us reach the maximum level, break
                if (level.CurrentValue == level.maxValue) {
                    break;
                }
                // increment the next level, see if we can go up another level
                nextLevel++;
                expToNextLevel = Constants.ExperienceToLevel[nextLevel];
            }
        }
    }

    /**
     * User gets experience even if they don't complete all their goals.
     * Calculate ratio of goal acheived, add up total experience
     */ 
    private void updateExperience(Cache cache, DateTime date) {
        Fitbit fitbit = cache.fitbit;

        List<double> steps = GetRelevantData(fitbit.activeSteps, date);
        List<double> distance = GetRelevantData(fitbit.activeDistance, date);
        List<double> fairlyActiveMinutes = GetRelevantData(fitbit.fairlyActive, date);
        List<double> realActiveMinutes = GetRelevantData(fitbit.veryActive, date);

        List<double> activeMinutes = AddLists(fairlyActiveMinutes, realActiveMinutes);

        double expFromSteps = GetDailyGoalRatio(steps, cache.dailyGoals.stepGoal.goal);
        double expFromDistance = GetDailyGoalRatio(distance, cache.dailyGoals.distanceGoal.goal);
        double expFromMinutes = GetDailyGoalRatio(activeMinutes, cache.dailyGoals.activeMinGoal.goal);

        // add new experience to previous experience
        experience.CurrentValueDouble += (expFromDistance + expFromSteps + expFromMinutes) * Constants.ExperiencePerGoalRatio;
    }



    /**
     * Updates health statistic. Checks for the time of day.
     * This is the stat that refills naturally over the day.
     * Rewards 4 hp per hour, or refills entirely at a change of date.
     */ 
    private void updateHealth(Cache cache, DateTime date) {
        DateTime today = DateTime.Now;
        if (today.Date != date.Date) {
            health.CurrentValue = Constants.MaxStat;
        } else {
            TimeSpan elapsed = new TimeSpan(today.Ticks - date.Ticks);
            health.CurrentValue += (4 * elapsed.Hours);
        }
    }

    /**
     * Strength is updated based on how much of the step goal has been acheived per day
     */ 
    private void updateStrength(Cache cache, DateTime date) {
        List<double> steps = GetRelevantData(cache.fitbit.activeSteps, date);
        double goalRatio = GetDailyGoalRatio(steps, cache.dailyGoals.stepGoal.goal);

        double strengthChange = (goalRatio-1) * Constants.StatChangePerGoalRatio;

        double bonusExperience = 0.0;

        if (strengthChange > 0 ) {
            bonusExperience = goalRatio * Constants.BonusExperience;
        }

        experience.CurrentValueDouble += bonusExperience;
        strength.CurrentValueDouble += strengthChange;
    }

    /**
     * Agility is updated based on active minutes goals
     */ 
    private void updateAgility(Cache cache, DateTime date) {
        List<double> fairlyActiveMinutes = GetRelevantData(cache.fitbit.fairlyActive, date);
        List<double> realActiveMinutes = GetRelevantData(cache.fitbit.veryActive, date);

        List<double> activeMinutes = AddLists(fairlyActiveMinutes, realActiveMinutes);

        double goalRatio = GetDailyGoalRatio(activeMinutes, cache.dailyGoals.activeMinGoal.goal);

        double agilityChange = (goalRatio - 1) * Constants.StatChangePerGoalRatio;

        double bonusExperience = 0.0;

        if (agilityChange > 0 ) {
            bonusExperience = goalRatio * Constants.BonusExperience;
        }

        experience.CurrentValueDouble += bonusExperience;
        agility.CurrentValueDouble += agilityChange;
    }

    /**
     * Defense is updated based on distance goals
     */ 
    private void updateDefense(Cache cache, DateTime date) {
        List<double> distance = GetRelevantData(cache.fitbit.activeDistance, date);

        double goalRatio = GetDailyGoalRatio(distance, cache.dailyGoals.distanceGoal.goal);

        double defenseChange = (goalRatio - 1) * Constants.StatChangePerGoalRatio;

        double bonusExperience = 0.0;

        if (defenseChange > 0 ) {
            bonusExperience = goalRatio * Constants.BonusExperience;
        }

        experience.CurrentValueDouble += bonusExperience;
        defense.CurrentValueDouble += defenseChange;
    }
    
    public static T GetTodaysData<T>(List<FitbitPair<T>> pairs) {
        T data = default(T);
        DateTime currentDate = DateTime.Today;
        foreach( FitbitPair<T> pair in pairs ) {
            if (pair.date == currentDate) {
                 data = pair.value;
            }
        }
        return data;
    }

    /**
     * For simplicity, don't look at today's data.
     * It will be handled when the user logs in the next time
     */ 
    private static List<double> GetRelevantData(List<FitbitPair<double>> pairs, DateTime date) {
        List <double> data = new List<double>();
        DateTime currentDate = DateTime.Today;
        foreach( FitbitPair<double> pair in pairs ) {
            if (pair.date >= date && pair.date != currentDate) {
                data.Add(pair.value);
            }
        }
        return data;
    }

    private static List<double> GetRelevantData(List<FitbitPair<int>> pairs, DateTime date) {
        List<double> data = new List<double>();
        DateTime currentDate = DateTime.Today;
        foreach (FitbitPair<int> pair in pairs) {
            if (pair.date >= date && pair.date != currentDate) {
                data.Add(pair.value);
            }
        }
        return data;
    }

    /**
     * Get ratio of a series of data and its associated daily goal
     */
    private double GetDailyGoalRatio(List<double> data, double goal) {
        double totalRatio = 0;
        double numRatio = 0;
        foreach ( double entry in data ) {
            totalRatio += (entry / goal);
            numRatio++;
        }
        return totalRatio / numRatio;
    }

    // assumes lists are the same length
    private List<double> AddLists(List<double> list1, List<double> list2) {
        List<double> combined = new List<double>();

        for (int i = 0; i < list1.Count; i++) {
            combined.Add(list1[i] + list2[i]);
        }
        return combined;
    }
}