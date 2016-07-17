using System.Collections.Generic;

public class Fitbit {
    /*
     *  Lifetime stats.
     */
    public LifetimeStats lifetime { get; set; }

    /*
     *  Calories.
     */
     public List<FitbitPair<int>> calories { get; set; }

    /*
     *  Calories from activities.
     */
    public List<FitbitPair<int>> activityCalories { get; set; }

    /*
     *  Steps.
     */
    public List<FitbitPair<int>> activeSteps { get; set; }

    /*
     *  Distance.
     */
    public List<FitbitPair<double>> activeDistance { get; set; }

    /*
     *  Compose active.
     */
    public List<FitbitPair<int>> fairlyActive { get; set; }
    public List<FitbitPair<int>> veryActive { get; set; }

    /**
     *  Compose it empty and construct lists in cache.
     */
    public Fitbit() {
        this.lifetime = null;
        this.calories = new List<FitbitPair<int>>();
        this.activityCalories = new List<FitbitPair<int>>();
        this.activeSteps = new List<FitbitPair<int>>();
        this.activeDistance = new List<FitbitPair<double>>();
        this.fairlyActive = new List<FitbitPair<int>>();
        this.veryActive = new List<FitbitPair<int>>();
    }

}
