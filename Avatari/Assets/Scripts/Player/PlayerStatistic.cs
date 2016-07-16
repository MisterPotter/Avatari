

using System.Diagnostics;
/**
* @author: Charlotte
* @summary: Stores the player statistics
*/
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

    public void updatePlayerStatistics() {
        updateExperience();
        updateHealth();
        updateStrength();
        updateAgility();
        updateDefense();
        updateLevel();
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

    private void updateExperience() {

    }

    /**
     * Updates health statistic. Checks for the time of day.
     * This is the stat that refills naturally over the day.
     */ 
    private void updateHealth() {

    }

    private void updateStrength() {

    }

    private void updateAgility() {

    }

    private void updateDefense() {

    }
}
