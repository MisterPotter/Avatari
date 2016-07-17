/**
 *  @author: Tyler
 *  @summary: A representation of an item to be stored in inventory.
 */
public class Item {
    /**
     *  The name of the item as the user sees it.
     */
    public string itemName { get; private set; }

    /**
     *  The name of the resource in the resources assets folder.
     */
    public string resourceName { get; private set; }

    /**
     *  The description of the resource as the user sees it.
     */
    public string itemDescription { get; private set; }

    /**
     *  A unique ID for the item.
     */
    public int itemID { get; set; }

    /**
     *  The type of the item, which represents how a character interacts with
     *  it.
     */
    public ItemType itemType { get; set; }

    /**
     *  How rare the item is. This becomes more important when considering the
     *  affects an item has on a player and factoring in random drops.
     */
    public ItemRarity itemRarity { get; set; }

    public enum ItemType {
        Head,
        Neck,
        Body,
        Feet,
        Hands,
        Ring,
        Wings,
        Consumable,
        Other
    }

    public enum ItemRarity {
        Common,
        Uncommon,
        Rare
    }

    public Item(
            string name, string resourceName, string description,
            int id, ItemType type, ItemRarity rarity) {
        this.itemName = name;
        this.resourceName = resourceName;
        this.itemDescription = description;
        this.itemID = id;
        this.itemType = type;
        this.itemRarity = rarity;
    }

    /**
     *  @summary: Determines if an item can be equipped.
     *  @return: True if a characters can equipt the item, False otherwise.
     */
    public bool IsEquippable() {
        return this.itemType != ItemType.Consumable &&
               this.itemType != ItemType.Other;
    }
}
