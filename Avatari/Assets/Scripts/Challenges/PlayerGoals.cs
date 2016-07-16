using System;

public class PlayerGoals {
    public DailyStats dailyStats;
    public LifetimeStats lifetimeStats;

    public PlayerGoals() {
        dailyStats = new DailyStats();
        lifetimeStats = new LifetimeStats();
    }

    public PlayerGoals(int dailySteps, int dailyCalories, int dailyDistance, int dailyActiveMinutes, int dailyStepsGoal, int dailyCaloriesGoal, int dailyDistanceGoal, int dailyActiveMinutesGoal) {
        dailyStats = new DailyStats(dailySteps, dailyCalories, dailyDistance, dailyActiveMinutes, dailyStepsGoal, dailyCaloriesGoal, dailyDistanceGoal, dailyActiveMinutesGoal);
    }

    public PlayerGoals(long lifetimeSteps, long lifetimeCalories, long lifetimeDistance, long lifetimeFloors, long lifetimeStepsGoal, long lifetimeCaloriesGoal, long lifetimeDistanceGoal, long lifetimeFloorsGoal) {
        lifetimeStats = new LifetimeStats(lifetimeSteps, lifetimeCalories, lifetimeDistance, lifetimeFloors, lifetimeStepsGoal, lifetimeCaloriesGoal, lifetimeDistanceGoal, lifetimeFloorsGoal);
    }
}
