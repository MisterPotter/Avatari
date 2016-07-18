using System;
using System.Collections.Generic;

/**
* @author: Denholm
* @summary: A wrapper to associate player goals, but isn't really used at the moment
* */
public class PlayerGoals {
    public List<Goal> dailyGoals;
    public List<Goal> challenges;
    public List<Goal> lifetimeGoals;

    public PlayerGoals() {
        dailyGoals = new List<Goal>();
        challenges = new List<Goal>();
        lifetimeGoals = new List<Goal>();
    }

    public PlayerGoals(List<Goal> dailyGoals, List<Goal> challenges, List<Goal> lifetimeGoals) {
        this.dailyGoals = dailyGoals;
        this.challenges = challenges;
        this.lifetimeGoals = lifetimeGoals;
    }
}
