/**
 *  @author: Tyler
 *  A class to represent an Area.
 */
public class Area {

    public int id { get; private set; }
    public string name { get; private set; }
    public string spriteName { get; private set; }
    public string description { get; private set; }

    public Area(int id, string name, string spriteName, string description) {
        this.id = id;
        this.name = name;
        this.spriteName = spriteName;
        this.description = description;
    }

}
