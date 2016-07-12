using System;
/**
* @author: Charlotte
* @summary: Defines a base class for player statistics
* */
public abstract class Statistic {

    // Attributes for each kind of statistic. Only ever set at init
    public readonly int minValue;
    public readonly int maxValue;
    public readonly string description;

    // Current value of the statistic
    private int currentValue;
    
    public int CurrentValue {
        get {
            return this.currentValue;
        }
        set {
            this.currentValue = CheckBounds(value);
        }
    }

    public Statistic(int minValue, int maxValue, string description) {
        this.minValue = minValue;
        this.description = description;
        this.currentValue = minValue;
    }

    /**
     * Increase or decrease the statistic by a value. 
     * Checks that currentValue never exceeds maxValue, or is less than minValue.
     */
    public void AddToValue(int amount) {
        int newValue = this.currentValue + amount;
        this.currentValue = CheckBounds(newValue);
    }

    private int CheckBounds(int newValue) {
        if (newValue > this.maxValue) {
            return this.maxValue;
        } else if (newValue < this.minValue) {
            return this.minValue;
        } else {
            return newValue;
        }
    }

}

// TODO: probably don't want level to ever decrease, should override change, add methods to ensure this.
public class Level : Statistic {

    public Level() : base(Constants.MinLevel,
                          Constants.MaxLevel,
                          Constants.LevelDescription) {}
}

// TODO: probably don't want experience to ever decrease, should override change, add methods to ensure this.
public class Experience : Statistic {

    public Experience() : base(Constants.MinExperience,
                               Constants.MaxExperience,
                               Constants.ExperienceDescription) {}
}

public class Health : Statistic {
    public Health() : base(Constants.MinStat,
                           Constants.MaxStat,
                           Constants.HealthDescription) {}
}

public class Agility : Statistic {
    public Agility() : base(Constants.MinStat,
                            Constants.MaxStat,
                            Constants.AgilityDescription) {}
}

public class Strength : Statistic {
    public Strength() : base(Constants.MinStat,
                             Constants.MaxStat,
                             Constants.StrengthDescription) {}
}

public class Defense : Statistic {
    public Defense() : base(Constants.MinStat,
                            Constants.MaxStat,
                            Constants.DefenseDescription) { }
}

/**
 * @summary: Static class to store constants associated with statistics
 * TODO: Find a better way to do this?
 */
public static class Constants {
    public const int MaxLevel = 20;
    public const int MinLevel = 0;
    public const int MaxExperience = 20000;
    public const int MinExperience = 0;
    public const int MaxStat = 20;
    public const int MinStat = 0;

    public const string LevelDescription = "Levels allow you to fight more bosses! Your level increases with experience.";
    public const string ExperienceDescription = "Experience lets you level up! Experience is increased by beating bosses, completing challenges and activities, and equipping items.";
    public const string HealthDescription = "Your health refills over the course of the day. You don't even have to do anything!";
    public const string AgilityDescription = "Agility increases as you do more cardio activities. Keep that heart rate up!";
    public const string StrengthDescription = "You get stronger as you take more steps. Make sure you go above and beyond your step goal!";
    public const string DefenseDescription = "Your defense increases when you eat well, sleep lots, and drink water.";

    /**
     * Array containing the number of experience to reach the level equal to the index.
     * eg. ExperienceToLevel[1] = 100 => there is 100 experience needed to reach level 1
     */
    public static readonly int[] ExperienceToLevel = {0, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000, 15000, 16000, 17000, 18000, 19000, 20000};
}
