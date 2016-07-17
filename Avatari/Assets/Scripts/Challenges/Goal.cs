using System;

public abstract class Goal {
    public const uint EMPTY = 0;
    public uint steps { get; set; }
    public uint stepGoal;
    public uint calories { get; set; }
    public uint calorieGoal;
    public uint distance { get; set; }
    public uint distanceGoal;
    public uint activeMin { get; set; }
    public uint activeMinGoal;

    public Goal() {
        this.steps = EMPTY;
        this.stepGoal = EMPTY;
        this.calories = EMPTY;
        this.calorieGoal = EMPTY;
        this.distance = EMPTY;
        this.distanceGoal = EMPTY;
        this.activeMin = EMPTY;
        this.activeMinGoal = EMPTY;
    }

    public Goal(uint stepGoal, uint calorieGoal, uint distanceGoal, uint activeMinGoal) {
        this.steps = EMPTY;
        this.calories = EMPTY;
        this.distance = EMPTY;
        this.activeMin = EMPTY;
        this.stepGoal = stepGoal;
        this.calorieGoal = calorieGoal;
        this.distanceGoal = distanceGoal;
        this.activeMinGoal = activeMinGoal;
    }
}

public class DailyGoal : Goal {

    public DailyGoal() : base() { }

    public DailyGoal(uint dailyStepsGoal, uint dailyCalorieGoal, uint dailyDistanceGoal, uint dailyActiveMinGoal) :
        base(dailyStepsGoal, dailyCalorieGoal, dailyDistanceGoal, dailyActiveMinGoal) { }

}

public class LifetimeGoal : Goal {

    public LifetimeGoal() : base() { }

    public LifetimeGoal(uint LTStepGoal, uint LTCalorieGoal, uint LTDistanceGoal, uint LTActiveMinGoal) :
        base(LTStepGoal, LTCalorieGoal, LTDistanceGoal, LTActiveMinGoal) { }
}

