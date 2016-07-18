using UnityEngine;

public class LifetimeStats {

    public int activeScore { get; private set; }
    public int caloriesOut { get; private set; }
    public float distance { get; private set; }
    public int floor { get; private set; }
    public int steps { get; private set; }

    public LifetimeStats(int score, int calories, float distance, int floor, int steps) {
        this.activeScore = score;
        this.caloriesOut = calories;
        this.distance = distance;
        this.floor = floor;
        this.steps = steps;
    }

}
