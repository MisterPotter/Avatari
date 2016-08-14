using UnityEngine;
using UnityEngine.UI;
using System;

/**
 *  @author: Tyler
 *
 *  This class loads the equipped player gears into the UI slots on the screen.
 */
public class LoadEquippedPanel : MonoBehaviour {

    private Image characterImage;
    private GameObject headSlot;
    private GameObject bodySlot;
    private GameObject feetSlot;
    private GameObject weaponSlot;
    private GameObject neckSlot;
    private GameObject wingsSlot;
    private GameObject ringSlot;
    private GameObject shieldSlot;

    private Cache cache;

    private void Awake() {
        FindCache();
        FindSlots();
        PopulateCharacterSlot();
        PopulateEquippedSlots();
    }

    /*
     *  Load our Cache.
     */
    private void FindCache() {
        this.cache = Utility.LoadObject<Cache>("Cache");
    }

    /*
     *  Load the UI Slots.
     */
    private void FindSlots() {
        this.characterImage = Utility.LoadObject<Image>(
            EquippedSlot.CharacterSlot
        );

        this.headSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.HeadSlot
        );
        this.bodySlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.BodySlot
        );
        this.feetSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.FeetSlot
        );
        this.weaponSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.WeaponSlot
        );
        this.neckSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.NecklaceSlot
        );
        this.wingsSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.WingsSlot
        );
        this.ringSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.RingsSlot
        );
        this.shieldSlot = GameObject.FindGameObjectWithTag(
            EquippedSlot.ShieldSlot
        );
    }

    /**
     *  Populate the character sprite.
     */
    private void PopulateCharacterSlot() {
        string spriteName = this.cache.LoadCharacterSprite();
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Characters/" + spriteName);
        if(spriteSheet.Length == 0) {
            throw new Exception("Character sprite: " + spriteName +
                " could not be found.");
        }
        this.characterImage.sprite = Utility.GetSprite("idle", spriteSheet);
    }

    /*
     *  Populate loaded UI slots.
     */
    private void PopulateEquippedSlots() {
        Player.EquippedGear equippedGear = this.cache.LoadEquippedItems();

        LoadEquippedItem(Item.ItemType.Head, equippedGear);
        LoadEquippedItem(Item.ItemType.Body, equippedGear);
        LoadEquippedItem(Item.ItemType.Feet, equippedGear);
        LoadEquippedItem(Item.ItemType.Weapon, equippedGear);
        LoadEquippedItem(Item.ItemType.Wings, equippedGear);
        LoadEquippedItem(Item.ItemType.Neck, equippedGear);
        LoadEquippedItem(Item.ItemType.Ring, equippedGear);
        LoadEquippedItem(Item.ItemType.Shield, equippedGear);
    }

    private void LoadEquippedItem(Item.ItemType itemType, Player.EquippedGear equippedGear) {

        // Choose our slot to load into, the item we want to examine
        // and our default resource incase that item is null.
        Image slot;
        EquippedSlot slotScript;
        Item item;
        string defaultResource;
        switch(itemType) {
            case Item.ItemType.Head:
                slot = this.headSlot.GetComponent<Image>();
                slotScript = this.headSlot.GetComponent<EquippedSlot>();
                item = equippedGear.headGear;
                defaultResource = "helmet";
                break;
            case Item.ItemType.Body:
                slot = this.bodySlot.GetComponent<Image>();
                slotScript = this.bodySlot.GetComponent<EquippedSlot>();
                item = equippedGear.bodyGear;
                defaultResource = "armor";
                break;
            case Item.ItemType.Feet:
                slot = this.feetSlot.GetComponent<Image>();
                slotScript = this.feetSlot.GetComponent<EquippedSlot>();
                item = equippedGear.footGear;
                defaultResource = "boots";
                break;
            case Item.ItemType.Weapon:
                slot = this.weaponSlot.GetComponent<Image>();
                slotScript = this.weaponSlot.GetComponent<EquippedSlot>();
                item = equippedGear.weaponGear;
                defaultResource = "weapon";
                break;
            case Item.ItemType.Neck:
                slot = this.neckSlot.GetComponent<Image>();
                slotScript = this.neckSlot.GetComponent<EquippedSlot>();
                item = equippedGear.neckGear;
                defaultResource = "necklace";
                break;
            case Item.ItemType.Wings:
                slot = this.wingsSlot.GetComponent<Image>();
                slotScript = this.wingsSlot.GetComponent<EquippedSlot>();
                item = equippedGear.wingGear;
                defaultResource = "wings";
                break;
            case Item.ItemType.Ring:
                slot = this.ringSlot.GetComponent<Image>();
                slotScript = this.ringSlot.GetComponent<EquippedSlot>();
                item = equippedGear.ringGear;
                defaultResource = "ring";
                break;
            case Item.ItemType.Shield:
                slot = this.shieldSlot.GetComponent<Image>();
                slotScript = this.shieldSlot.GetComponent<EquippedSlot>();
                item = equippedGear.shieldGear;
                defaultResource = "shield";
                break;
            default:
                Debug.LogError("Invalid equipped item type for player: "
                    + itemType);
                return;
        }

        // If nothing is equipt, use the default resource
        if (item == null) {
            slot.sprite = Resources.Load<Sprite>("Inventory/" + defaultResource);
            slotScript.item = null;
            slotScript.slot = slot;
            slotScript.slotDefault = slot.sprite;
            return;
        }

        // Enforce the item type to be the type we are expecting and load it
        if (item.itemType == itemType) {
            slot.sprite = this.cache.LoadInventorySprite(item.resourceName);
            slotScript.slot = slot;
            slotScript.slotDefault = Resources.Load<Sprite>("Inventory/" + defaultResource);
            slotScript.item = item;
        } else {
            throw new Exception("Expected player gear of type: " + itemType
                + " but got gear of type: " + item.itemType);
        }
    }
}
