using System;

/**
 *  @author Tyler
 */
public class Player {

    public class EquippedGear {
        public Item headGear;
        public Item chestGear;
        public Item footGear;
        public Item handGear;
        public Item wingGear;

        public EquippedGear(Item head, Item chest, Item foot, Item hand, Item wing) {
            this.headGear = head;
            this.chestGear = chest;
            this.footGear = foot;
            this.handGear = hand;
            this.wingGear = wing;
        }
    }

    /**
     *  The gear that the player current has equipped.
     */
    public EquippedGear gear { get; set; }

    /**
     *  The name of the sprite to be loaded for the player.
     */
    public string sprite { get; set; }

    public Area area { get; set; }

    /**
     * The statistics of the player 
     */
    public PlayerStatistic stats;

    public Player() {
        this.gear = null;
        this.sprite = null;
        this.stats = null;
        this.area = null;
    }

    /**
     *  Removed an item.
     */
    public void UnequipItem(Item item) {
        bool itemUnequipped = false;
        switch(item.itemType) {
            case Item.ItemType.Head:
                if(item.itemID == this.gear.headGear.itemID) {
                    this.gear.headGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Body:
                if (item.itemID == gear.chestGear.itemID) {
                    this.gear.chestGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Hands:
                if (item.itemID == gear.handGear.itemID) {
                    this.gear.handGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Feet:
                if (item.itemID == gear.footGear.itemID) {
                    this.gear.footGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Wings:
                if (item.itemID == gear.wingGear.itemID) {
                    this.gear.wingGear = null;
                    itemUnequipped = true;
                }
                break;
            default:
                throw new Exception("Invalid item type while uneqipping: "
                    + item.itemType);
        }
        if (itemUnequipped) {
            this.stats.UpdateStatOnItemChange(item, false);
        }
    }

    /**
     *  Equips an item. Removed one first if one is already equipped.
     */
    public void EquipItem(Item item) {
        Item currentlyEquipped = GetEquipped(item.itemType);
        bool itemEquipped = false;

        switch (item.itemType) {
            case Item.ItemType.Head:
                if(currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.headGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Body:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.chestGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Hands:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.handGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Feet:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.footGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Wings:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.wingGear = item;
                itemEquipped = true;
                break;
            default:
                throw new Exception("Invalid item type while eqipping: "
                    + item.itemType);
        }

        if (itemEquipped) {
            this.stats.UpdateStatOnItemChange(item, true);
        }
    }

    /**
     *  Get the currently equipped item in a particular slot.
     */
    public Item GetEquipped(Item.ItemType type) {
        switch (type) {
            case Item.ItemType.Head:
                return this.gear.headGear;
            case Item.ItemType.Body:
                return this.gear.chestGear;
            case Item.ItemType.Hands:
                return this.gear.handGear;
            case Item.ItemType.Feet:
                return this.gear.footGear;
            case Item.ItemType.Wings:
                return this.gear.wingGear;
            default:
                throw new Exception("Invalid item type while eqipping: "
                    + type);
        }
    }
}
