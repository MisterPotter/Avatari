using System;
using System.Collections;
using System.Collections.Generic;

/**
 *  @author Tyler
 */
public class Player {

    public class EquippedGear : IEnumerable<Item> {
        public Item headGear;
        public Item bodyGear;
        public Item footGear;
        public Item weaponGear;
        public Item neckGear;
        public Item wingGear;
        public Item ringGear;
        public Item shieldGear;
        // It may be better to implement this as a derived value
        // from its own iterable to improve the ability to refactor
        public readonly uint Count = 8U;

        public EquippedGear() {
            this.headGear = null;
            this.bodyGear = null;
            this.footGear = null;
            this.weaponGear = null;
            this.neckGear = null;
            this.wingGear = null;
            this.ringGear = null;
            this.shieldGear = null;
        }

        public EquippedGear(
                Item head, Item body, Item foot, Item weapon,
                Item neck, Item wing, Item ring, Item sheild) {
            this.headGear = head;
            this.bodyGear = body;
            this.footGear = foot;
            this.weaponGear = weapon;
            this.neckGear = neck;
            this.wingGear = wing;
            this.ringGear = ring;
            this.shieldGear = sheild;
        }

        public IEnumerator<Item> GetEnumerator() {
            yield return this.headGear == null? new Item(Item.ItemType.Head) : this.headGear;
            yield return this.bodyGear == null ? new Item(Item.ItemType.Body) : this.bodyGear;
            yield return this.footGear == null ? new Item(Item.ItemType.Feet) : this.footGear;
            yield return this.weaponGear == null ? new Item(Item.ItemType.Weapon) : this.weaponGear;
            yield return this.neckGear == null ? new Item(Item.ItemType.Neck) : this.neckGear;
            yield return this.wingGear == null ? new Item(Item.ItemType.Wings) : this.wingGear;
            yield return this.ringGear == null ? new Item(Item.ItemType.Ring) : this.ringGear;
            yield return this.shieldGear == null ? new Item(Item.ItemType.Shield) : this.shieldGear;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    /**
     *  The gear that the player current has equipped.
     */
    public EquippedGear gear { get; set; }

    public string username { get; set; }

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

    public Player(
            EquippedGear gear, string username, string spriteName,
            PlayerStatistic stats, Area area) {
        this.gear = gear;
        this.username = username;
        this.sprite = spriteName;
        this.stats = stats;
        this.area = area;
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
                if (item.itemID == gear.bodyGear.itemID) {
                    this.gear.bodyGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Feet:
                if (item.itemID == gear.footGear.itemID) {
                    this.gear.footGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Weapon:
                if (item.itemID == gear.weaponGear.itemID) {
                    this.gear.weaponGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Neck:
                if (item.itemID == gear.neckGear.itemID) {
                    this.gear.neckGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Wings:
                if (item.itemID == gear.wingGear.itemID) {
                    this.gear.wingGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Ring:
                if (item.itemID == gear.ringGear.itemID) {
                    this.gear.ringGear = null;
                    itemUnequipped = true;
                }
                break;
            case Item.ItemType.Shield:
                if (item.itemID == gear.shieldGear.itemID) {
                    this.gear.shieldGear = null;
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
                this.gear.bodyGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Feet:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.footGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Weapon:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.weaponGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Neck:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.neckGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Wings:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.wingGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Ring:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.ringGear = item;
                itemEquipped = true;
                break;
            case Item.ItemType.Shield:
                if (currentlyEquipped != null) {
                    UnequipItem(currentlyEquipped);
                }
                this.gear.shieldGear = item;
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
                return this.gear.bodyGear;
            case Item.ItemType.Feet:
                return this.gear.footGear;
            case Item.ItemType.Weapon:
                return this.gear.weaponGear;
            case Item.ItemType.Neck:
                return this.gear.neckGear;
            case Item.ItemType.Wings:
                return this.gear.wingGear;
            case Item.ItemType.Ring:
                return this.gear.ringGear;
            case Item.ItemType.Shield:
                return this.gear.shieldGear;
            default:
                throw new Exception("Invalid item type while eqipping: "
                    + type);
        }
    }
}
