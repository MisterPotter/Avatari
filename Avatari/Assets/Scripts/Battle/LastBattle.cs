/**
 * Stores data about the last battle
 * Used for generating a reward dialog upon battle exit
 */ 
public static class LastBattle {
    public static bool rewardCollected = false;
    public static bool battleWon = false;
    public static Boss boss;
}
