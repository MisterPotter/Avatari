/**
 *  @author: Tyler
 *  @summary: A representation of an item to be stored in inventory.
 */
public class Item {
    private string itemName;
    private int itemID;
    private ItemType itemType;

    public enum ItemType {
        Head,
        Body,
        Feet,
        Hands
    }

    public Item(string name, int id, ItemType type) {
        this.itemName = name;
        this.itemID = id;
        this.itemType = type;
    }

}
