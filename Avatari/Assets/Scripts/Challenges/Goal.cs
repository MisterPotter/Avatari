using System;
using System.Collections.Generic;
/**
* @author: Denholm
* @summary: Defines a template goal and the daily/challenge/lifetime goals that extend it
* */
public class Goal {
    private const float EMPTY = 0.0f;
    public float progress;
    public float goal;
    public string description;

    public Goal() {
        this.progress = EMPTY;
        this.goal = EMPTY;
        this.description = "";
    }

    public Goal(string description, float goal) {
        this.description = description;
        this.progress = EMPTY;
        this.goal = goal;
    }

    public Goal(string description, float progress, float goal) {
        this.description = description;
        this.progress = progress;
        this.goal = goal;
    }
}

public class DailyGoals {
    public Goal stepGoal { get; set; }
    public Goal calorieGoal { get; set; }
    public Goal distanceGoal { get; set; }
    public Goal activeMinGoal { get; set; }

    public DailyGoals(){
        this.stepGoal = new Goal();
        this.calorieGoal = new Goal();
        this.distanceGoal = new Goal();
        this.activeMinGoal = new Goal();
    }

    public DailyGoals(float dailyStepsGoal, float dailyCalorieGoal, float dailyDistanceGoal, float dailyActiveMinGoal){
        this.stepGoal = new Goal("Step goal", dailyStepsGoal);
        this.calorieGoal = new Goal("Calorie goal", dailyCalorieGoal);
        this.distanceGoal = new Goal("Distance goal (km)", dailyDistanceGoal);
        this.activeMinGoal = new Goal("Active minutes goal", dailyActiveMinGoal);
    }
}

//NOTE: Though this currently duplicates Daily goals it might differ in the future and thus is kept separate
public class LifetimeGoals {
    public Goal stepGoal { get; set; }
    public Goal calorieGoal { get; set; }
    public Goal distanceGoal { get; set; }
    public Goal activeMinGoal { get; set; }

    public LifetimeGoals() {
        this.stepGoal = new Goal();
        this.calorieGoal = new Goal();
        this.distanceGoal = new Goal();
        this.activeMinGoal = new Goal();
    }

    public LifetimeGoals(float LTStepGoal, float LTCalorieGoal, float LTDistanceGoal, float LTActiveMinGoal) {
        this.stepGoal = new Goal("Step goal", LTStepGoal);
        this.calorieGoal = new Goal("Calorie goal", LTCalorieGoal);
        this.distanceGoal = new Goal("Distance goal (km)", LTDistanceGoal);
        this.activeMinGoal = new Goal("Active minutes goal", LTActiveMinGoal);
    }
}

public class Challenges {
    public Goal biking { get; set; }
    public Goal running { get; set; }
    public Goal hiking { get; set; }
    public Goal swimming { get; set; }

    public Challenges() {
        this.biking = new Goal();
        this.running = new Goal();
        this.hiking = new Goal();
        this.swimming = new Goal();
    }

    public Challenges(float bikingGoal, float runningGoal, float hikingGoal, float swimmingGoal) {
        this.biking = new Goal("Biking goal", bikingGoal);
        this.running = new Goal("Running goal", runningGoal);
        this.hiking = new Goal("Hiking goal", hikingGoal);
        this.swimming = new Goal("Swimming goal", swimmingGoal);
    }
}

