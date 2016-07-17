/**
 *  @author: Tyler
 *  @summary: Enpsulate the information about the taris that the user
 *      can choose.
 */
public class Tari {

    public int id { get; private set; }
    public string name { get; private set; }
    public string spriteName { get; private set; }
    public string description { get; private set; }

    public Tari(int id, string name, string spriteName, string description) {
        this.id = id;
        this.name = name;
        this.spriteName = spriteName;
        this.description = description;
    }

}
