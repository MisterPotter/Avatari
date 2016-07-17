using System;
using System.Collections.Generic;

public class PlayerGoals {
    public List<DailyGoal> dailyGoals;
    public List<LifetimeGoal> lifetimeGoals;

    public PlayerGoals() {
        dailyGoals = new List<DailyGoal>();
        lifetimeGoals = new List<LifetimeGoal>();
    }

    public PlayerGoals(List<DailyGoal> dailyGoals, List<LifetimeGoal> lifetimeGoals) {
        this.dailyGoals = dailyGoals;
        this.lifetimeGoals = lifetimeGoals;
    }
}
