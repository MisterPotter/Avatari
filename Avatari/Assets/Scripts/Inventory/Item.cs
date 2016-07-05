/**
 *  @author: Tyler
 *  @summary: A representation of an item to be stored in inventory.
 */
public class Item {
    /**
     *  The name of the item as the user sees it.
     */
    private string _itemName;
    public string itemName {
        get { return _itemName; }
        private set { _itemName = value; }
    }

    /**
     *  The name of the resource in the resources assets folder.
     */
    private string _resourceName;
    public string resourceName {
        get { return _resourceName; }
        private set { _resourceName = value; }
    }

    /**
     *  The description of the resource as the user sees it.
     */
    private string _itemDescription;
    public string itemDescription {
        get { return _itemDescription; }
        private set { _itemDescription = value; }
    }

    /**
     *  A unique ID for the item.
     */
    private int _itemID;
    public int itemID {
        get { return _itemID; }
        set { _itemID = value; }
    }

    /**
     *  The type of the item, which represents how a character interacts with
     *  it.
     */
    private ItemType _itemType;
    public ItemType itemType {
        get { return _itemType; }
        set { _itemType = value; }
    }

    public enum ItemType {
        Head,
        Body,
        Feet,
        Hands,
        Wings,
        Consumable
    }

    public Item(string name, string resourceName, string description, int id, ItemType type) {
        this.itemName = name;
        this.resourceName = resourceName;
        this.itemDescription = description;
        this.itemID = id;
        this.itemType = type;
    }

    /**
     *  @summary: Determines if an item can be equipped.
     *  @return: True if a characters can equipt the item, False otherwise.
     */
    public bool IsEquiptable() {
        return this.itemType != ItemType.Consumable;
    }
}
