using System.Collections;
/**
* @author: Denholm
* @summary: Defines a base class for a boss
* */
public class Boss {
    private string bossName;
    private string spriteName;
    private string requirement;
    private string info;
    private bool locked;
    private PlayerStatistic stats;
    private int bossID;
   
    public Boss (string name, string info, PlayerStatistic stats, int bossID) {
        this.bossName = name;
        this.spriteName = "monster" + bossID;
        this.info = info;
        this.bossID = bossID;
        this.stats = stats;
        this.requirement = "Reach level " + this.stats.level.CurrentValue;
        this.locked = true;
    }

    public string getSpriteName () {
        return this.spriteName;
    }

    public string getName() {
        return this.bossName;
    }

    public string getRequirement() {
        return this.requirement;
    }

    public string getInfo() {
        return this.info;
    }

    public PlayerStatistic getStats() {
        return this.stats;
    }

    public int getLevel() {
        return this.stats.level.CurrentValue;
    }
}
