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

    /**
     * What player statistic this item affects.
     */
    public Statistic.Type statType { get; set; } 

    /**
     * To what the degree the stat is affected.
     */
    public int statBoost { get; set; }

    public enum ItemType {
        Head,
        Neck,
        Body,
        Feet,
        Weapon,
        Shield,
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
            int id, ItemType type, ItemRarity rarity, Statistic.Type statType,
            int statBoost) {
        this.itemName = name;
        this.resourceName = resourceName;
        this.itemDescription = description;
        this.itemID = id;
        this.itemType = type;
        this.itemRarity = rarity;
        this.statType = statType;
        this.statBoost = statBoost;
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
