/**
 * @author: Charlotte
 * @summary: Static class to store constants associated with statistics and player
 */
public static class Constants {
    public const int MaxLevel = 20;
    public const int MinLevel = 0;
    public const int MaxExperience = 20000;
    public const int MinExperience = 0;
    public const int MaxStat = 100;
    public const int MinStat = 0;

    public const string LevelDescription = "Levels allow you to fight more bosses! Your level increases with experience.";
    public const string ExperienceDescription = "Experience lets you level up! Experience is increased by beating bosses, completing challenges and activities, and by just being active";
    public const string HealthDescription = "Your health refills over the course of the day. You don't even have to do anything!";
    public const string AgilityDescription = "Agility increases as you do more cardio activities. Keep that heart rate up!";
    public const string StrengthDescription = "You get stronger as you take more steps. Make sure you go above and beyond your step goal!";
    public const string DefenseDescription = "Your defense increases with your distance travelled. Get those kilometers in!";

    /* Constants associated with Fitbit goals for stats calculations */
    public const int StepGoal = 10000;
    public const int CalorieGoal = 2000;
    public const int DistanceGoal = 8;
    public const int ActiveMinutesGoal = 30;

    public const int LifeStepGoal = 2000000;
    public const int LifeCalorieGoal = 500000;
    public const int LifeDistanceGoal = 10000;
    public const int LifeActiveMinutesGoal = 10000;

    public const int BikeChallenge = 20;
    public const int RunningChallenge = 10;
    public const int HikingChallenge = 10;
    public const int SwimmingChallenge = 5; 

    public const int ExperiencePerGoalRatio = 50;
    public const int BonusExperience = 10;

    /* 
     * This represents how much your stat would change if you go above or below your goal.
     * ie. statchange = ( ratio of goal achieved - 1) * statChangePerGoalRatio
     * statchange = ( 1.5 - 1 ) * 2 = 1 stat point if 50% over goal.
     */
    public const int StatChangePerGoalRatio = 10;

    /**
     * Array containing the number of experience to reach the level equal to the index.
     * eg. ExperienceToLevel[1] = 100 => there is 100 experience needed to reach level 1
     */
    public static readonly int[] ExperienceToLevel = { 0, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000, 15000, 16000, 17000, 18000, 19000, 20000 };
}