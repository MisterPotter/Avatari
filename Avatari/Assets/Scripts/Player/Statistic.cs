using System;
/**
* @author: Charlotte
* @summary: Defines a base class for player statistics
* */

[Serializable]
public abstract class Statistic {

    // Attributes for each kind of statistic. Only ever set at init
    public readonly int minValue;
    public readonly int maxValue;
    public readonly string description;
    public readonly Type statType;

    // Current value of the statistic
    protected int currentValue;

    public int CurrentValue {
        get {
            return this.currentValue;
        }
        set {
            this.currentValue = CheckBounds(value);
            this.currentValueDouble = this.currentValue;
        }
    }

    /* 
     * More precise current value
     * currentValue is the floor of this value.
     */
    protected double currentValueDouble;

    public double CurrentValueDouble {
        get {
            return this.currentValueDouble;
        }
        set {
            this.currentValueDouble = CheckBounds(value);
            this.currentValue = (int) Math.Floor(this.currentValueDouble);
        }
    }

    public enum Type {
        Level,
        Experience,
        Health,
        Strength,
        Agility,
        Defense
    }

    public Statistic(int minValue, int maxValue, string description, Type statType) {
        this.maxValue = maxValue;
        this.minValue = minValue;
        this.description = description;
        this.statType = statType;
        this.currentValue = minValue;
        this.currentValueDouble = minValue;
    }

    public Statistic(int minValue, int maxValue, string description, Type statType, int initValue) {
        this.maxValue = maxValue;
        this.minValue = minValue;
        this.description = description;
        this.statType = statType;
        this.currentValue = initValue;
        this.currentValueDouble = initValue;
    }

    /**
     * Increase or decrease the statistic by a value. 
     * Checks that currentValue never exceeds maxValue, or is less than minValue.
     */
    public void AddToValue(int amount) {
        int newValue = this.currentValue + amount;
        this.currentValue = CheckBounds(newValue);
    }

    private double CheckBounds(double newValue) {
        if (newValue > this.maxValue) {
            return this.maxValue;
        } else if (newValue < this.minValue) {
            return this.minValue;
        } else {
            return newValue;
        }
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
[Serializable]
public class Level : Statistic {

    public Level() : base(Constants.MinLevel,
                          Constants.MaxLevel,
                          Constants.LevelDescription,
                          Type.Level) {
    }

    public Level(int level) : base(Constants.MinLevel,
                                   Constants.MaxLevel,
                                   Constants.LevelDescription,
                                   Type.Level,
                                   level) {
    }
}

// TODO: probably don't want experience to ever decrease, should override change, add methods to ensure this.
[Serializable]
public class Experience : Statistic {

    public Experience() : base(Constants.MinExperience,
                               Constants.MaxExperience,
                               Constants.ExperienceDescription,
                               Type.Experience) {
    }

    public Experience(int experience) : base(Constants.MinExperience,
                                             Constants.MaxExperience,
                                             Constants.ExperienceDescription,
                                             Type.Experience,
                                             experience) {
    }
}

[Serializable]
public class Health : Statistic {
    public Health() : base(Constants.MinStat,
                           Constants.MaxStat,
                           Constants.HealthDescription,
                           Type.Health) {
    }
    public Health(int health) : base(Constants.MinStat,
                                     Constants.MaxStat,
                                     Constants.HealthDescription,
                                     Type.Health,
                                     health) {
    }
}

[Serializable]
public class Agility : Statistic {
    public Agility() : base(Constants.MinStat,
                            Constants.MaxStat,
                            Constants.AgilityDescription,
                            Type.Agility) {
    }
    public Agility(int agility) : base(Constants.MinStat,
                                       Constants.MaxStat,
                                       Constants.AgilityDescription,
                                       Type.Agility,
                                       agility) {
    }
}

[Serializable]
public class Strength : Statistic {
    public Strength() : base(Constants.MinStat,
                             Constants.MaxStat,
                             Constants.StrengthDescription,
                             Type.Strength) {
    }
    public Strength(int strength) : base(Constants.MinStat,
                                         Constants.MaxStat,
                                         Constants.StrengthDescription,
                                         Type.Strength,
                                         strength) {
    }
}

[Serializable]
public class Defense : Statistic {
    public Defense() : base(Constants.MinStat,
                            Constants.MaxStat,
                            Constants.DefenseDescription,
                            Type.Defense) {
    }
    public Defense(int defense) : base(Constants.MinStat,
                                       Constants.MaxStat,
                                       Constants.DefenseDescription,
                                       Type.Defense,
                                       defense) {
    }
}
