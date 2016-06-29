using System.Collections;

public class Boss {
    private string bossName;
    private string requirement;
    private string info;
    private bool locked;
    private int level;
    private int bossID;
   
    public Boss (string name, string info, int level, int bossID) {
        this.bossName = name;
        this.info = info;
        this.bossID = bossID;
        this.level = level;
        this.requirement = "Reach level " + this.level;
        this.locked = true;
    }
}
