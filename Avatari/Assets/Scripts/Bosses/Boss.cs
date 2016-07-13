using System.Collections;

public class Boss {
    private string bossName;
    private string spriteName;
    private string requirement;
    private string info;
    private bool locked;
    private int level;
    private int bossID;
   
    public Boss (string name, string info, int level, int bossID) {
        this.bossName = name;
        this.spriteName = "monster" + bossID;
        this.info = info;
        this.bossID = bossID;
        this.level = level;
        this.requirement = "Reach level " + this.level;
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
}
