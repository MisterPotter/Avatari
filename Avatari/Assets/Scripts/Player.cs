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

    public Player() {
        this.gear = null;
        this.sprite = null;
    }

    public void UnequipItem(Item item) {
        switch(item.itemType) {
            case Item.ItemType.Head:
                if(item.itemID == this.gear.headGear.itemID) {
                    this.gear.headGear = null;
                }
                break;
            case Item.ItemType.Body:
                if (item.itemID == gear.chestGear.itemID) {
                    this.gear.chestGear = null;
                }
                break;
            case Item.ItemType.Hands:
                if (item.itemID == gear.handGear.itemID) {
                    this.gear.handGear = null;
                }
                break;
            case Item.ItemType.Feet:
                if (item.itemID == gear.footGear.itemID) {
                    this.gear.footGear = null;
                }
                break;
            case Item.ItemType.Wings:
                if (item.itemID == gear.wingGear.itemID) {
                    this.gear.wingGear = null;
                }
                break;
            default:
                throw new Exception("Invalid item type while uneqipping: "
                    + item.itemType);
        }
    }
}
