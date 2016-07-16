using System;

public abstract class Goals {
    public const int EMPTY_VALUE = 0;
    public const int RECOMENDED_DAILY_STEP_GOAL = 10000;
    public const int RECOMENDED_DAILY_CALORIE_GOAL = 2000;
    public const int RECOMENDED_DAILY_DISTANCE_GOAL = 5;
    public const int RECOMENDED_DAILY_ACTIVE_MINUTES_GOAL = 120;
    public const int RECOMENDED_LIFETIME_STEP_GOAL = 10000;
    public const int RECOMENDED_LIFETIME_CALORIE_GOAL = 2000;
    public const int RECOMENDED_LIFETIME_DISTANCE_GOAL = 5;
    public const int RECOMENDED_LIFETIME_FLOORS_GOAL = 120;
    public int dailySteps;
    public int dailyStepsGoal;
    public long lifetimeSteps;
    public long lifetimeStepsGoal;
    public int dailyCalories;
    public int dailyCaloriesGoal;
    public long lifetimeCalories;
    public long lifetimeCaloriesGoal;
    public int dailyDistance;
    public int dailyDistanceGoal;
    public long lifetimeDistance;
    public long lifetimeDistanceGoal;
    public int dailyActiveMinutes;
    public int dailyActiveMinutesGoal;
    public long lifetimeFloors;
    public long lifetimeFloorsGoal;

    public Goals(int dailySteps, int dailyCalories, int dailyDistance, int dailyActiveMinutes, int dailyStepsGoal, int dailyCaloriesGoal, int dailyDistanceGoal, int dailyActiveMinutesGoal) {
        this.dailySteps = dailySteps;
        this.dailyCalories = dailyCalories;
        this.dailyDistance = dailyDistance;
        this.dailyActiveMinutes = dailyActiveMinutes;

        this.dailyStepsGoal = dailyStepsGoal;
        this.dailyCaloriesGoal = dailyCaloriesGoal;
        this.dailyDistanceGoal = dailyDistanceGoal;
        this.dailyActiveMinutesGoal = dailyActiveMinutesGoal;
    }

    public Goals(long lifetimeSteps, long lifetimeCalories, long lifetimeDistance, long lifetimeFloors, long lifetimeStepsGoal, long lifetimeCaloriesGoal, long lifetimeDistanceGoal, long lifetimeFloorsGoal) {
        this.lifetimeSteps = lifetimeSteps;
        this.lifetimeCalories = lifetimeCalories;
        this.lifetimeDistance = lifetimeDistance;
        this.lifetimeFloors = lifetimeFloors;

        this.lifetimeStepsGoal = lifetimeStepsGoal;
        this.lifetimeCaloriesGoal = lifetimeCaloriesGoal;
        this.lifetimeDistanceGoal = lifetimeDistanceGoal;
        this.lifetimeFloorsGoal = lifetimeFloorsGoal;
    }

}

    public class DailyStats : Goals {
        public DailyStats() 
        : base(EMPTY_VALUE,
            RECOMENDED_DAILY_STEP_GOAL,
            EMPTY_VALUE,
            RECOMENDED_DAILY_CALORIE_GOAL,
            EMPTY_VALUE,
            RECOMENDED_DAILY_DISTANCE_GOAL,
            EMPTY_VALUE,
            RECOMENDED_DAILY_ACTIVE_MINUTES_GOAL){ }

        public DailyStats(int dailySteps, 
            int dailyCalories, 
            int dailyDistance, 
            int dailyFloors, 
            int dailyStepsGoal, 
            int dailyCaloriesGoal, 
            int dailyDistanceGoal, 
            int dailyActiveMinutesGoal)
        : base(dailySteps, 
            dailyCalories, 
            dailyDistance, 
            dailyFloors, 
            dailyStepsGoal, 
            dailyCaloriesGoal, 
            dailyDistanceGoal, 
            dailyActiveMinutesGoal) { }
    }

    public class LifetimeStats : Goals {
        public LifetimeStats() 
            : base(EMPTY_VALUE,
                RECOMENDED_LIFETIME_STEP_GOAL,
                EMPTY_VALUE,
                RECOMENDED_LIFETIME_CALORIE_GOAL,
                EMPTY_VALUE,
                RECOMENDED_LIFETIME_DISTANCE_GOAL,
                EMPTY_VALUE,
                RECOMENDED_LIFETIME_FLOORS_GOAL){ }


        public LifetimeStats(long lifetimeSteps, 
                long lifetimeCalories, 
                long lifetimeDistance, 
                long lifetimeFloors, 
                long lifetimeStepsGoal, 
                long lifetimeCaloriesGoal, 
                long lifetimeDistanceGoal, 
                long lifetimeFloorsGoal)
            : base(lifetimeSteps, 
                lifetimeCalories, 
                lifetimeDistance, 
                lifetimeFloors,
                lifetimeStepsGoal,
                lifetimeDistanceGoal,
                lifetimeFloorsGoal,
                lifetimeCaloriesGoal
                ) { }
    }

