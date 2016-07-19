using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/**
* @author: Charlotte
* @summary: Stores the player statistics
*/
[Serializable]
public class PlayerStatistic {
    public Level level;
    public Experience experience;
    public Health health;
    public Strength strength;
    public Agility agility;
    public Defense defense;

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

    /**
     * Called once the app is opened for the first time in a day
     * Need to be passed an instance of the cache to access the fitbit data,
     * as well as the date to update from
     */ 
    public void UpdatePlayerStatisticsSince(Cache cache, DateTime date) {
        updateExperience(cache, date);
        updateHealth();
        updateStrength(cache, date);
        updateAgility(cache, date);
        updateDefense(cache, date);
        updateLevel();
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
                this.experience.AddToValue(value);
                break;
            case Statistic.Type.Health:
                this.health.AddToValue(value);
                break;
            case Statistic.Type.Agility:
                this.agility.AddToValue(value);
                break;
            case Statistic.Type.Defense:
                this.defense.AddToValue(value);
                break;
            case Statistic.Type.Strength:
                this.strength.AddToValue(value);
                break;
            case Statistic.Type.Level:
                this.level.AddToValue(value);
                break;
            default:
                throw new Exception("Invalid Statistic type on item equip or unequip: " + item.statType);
        }
    }

    /**
     * Check the experience level and determine if level needs to be increased
     */
    private void updateLevel() {

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
                level.AddToValue(1);
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
     */ 
    private void updateHealth() {

    }

    /**
     * Strength is updated based on how much of the step goal has been acheived per day
     */ 
    private void updateStrength(Cache cache, DateTime date) {
        List<double> steps = GetRelevantData(cache.fitbit.activeSteps, date);
        double goalRatio = GetDailyGoalRatio(steps, cache.dailyGoals.stepGoal.goal);

        double strengthChange = (goalRatio-1) * Constants.StatChangePerGoalRatio;

        double bonusExperience = 0.0;

        // be a merciful god and take the ceiling if strength change is negative
        // (since we take the floor when rounding to int usually)
        if (strengthChange < 0) {
            strengthChange = Math.Ceiling(strengthChange);
        } else {
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

        // be a merciful god and take the ceiling if strength change is negative
        // (since we take the floor when rounding to int usually)
        if (agilityChange < 0) {
            agilityChange = Math.Ceiling(agilityChange);
        } else {
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

        // be a merciful god and take the ceiling if strength change is negative
        // (since we take the floor when rounding to int usually)
        if (defenseChange < 0) {
            defenseChange = Math.Ceiling(defenseChange);
        } else {
            bonusExperience = goalRatio * Constants.BonusExperience;
        }

        experience.CurrentValueDouble += bonusExperience;
        defense.CurrentValueDouble += defenseChange;
    }
    
    /**
     * For simplicity, don't look at today's data.
     * It will be handled when the user logs in the next time
     */ 
    private List<double> GetRelevantData(List<FitbitPair<double>> pairs, DateTime date) {
        List <double> data = new List<double>();
        DateTime currentDate = DateTime.Today;
        foreach( FitbitPair<double> pair in pairs ) {
            if (pair.date >= date && pair.date != currentDate) {
                data.Add(pair.value);
            }
        }
        return data;
    }

    private List<double> GetRelevantData(List<FitbitPair<int>> pairs, DateTime date) {
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
        double totalGoal = 0;
        double totalActual = 0;
        foreach ( double entry in data ) {
            totalActual += entry;
            totalGoal += goal;
        }
        return totalActual / totalGoal;
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