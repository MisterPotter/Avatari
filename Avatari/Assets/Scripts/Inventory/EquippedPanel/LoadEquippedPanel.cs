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
    private GameObject chestSlot;
    private GameObject feetSlot;
    private GameObject handsSlot;
    private GameObject wingsSlot;

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
        this.characterImage = Utility.LoadObject<Image>("CharacterSlot");

        this.headSlot = GameObject.FindGameObjectWithTag("HeadSlot");
        this.chestSlot = GameObject.FindGameObjectWithTag("ChestSlot");
        this.feetSlot = GameObject.FindGameObjectWithTag("FeetSlot");
        this.handsSlot = GameObject.FindGameObjectWithTag("HandsSlot");
        this.wingsSlot = GameObject.FindGameObjectWithTag("WingsSlot");
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
        LoadEquippedItem(Item.ItemType.Hands, equippedGear);
        LoadEquippedItem(Item.ItemType.Wings, equippedGear);
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
                defaultResource = "head";
                break;
            case Item.ItemType.Body:
                slot = this.chestSlot.GetComponent<Image>();
                slotScript = this.chestSlot.GetComponent<EquippedSlot>();
                item = equippedGear.chestGear;
                defaultResource = "chest";
                break;
            case Item.ItemType.Feet:
                slot = this.feetSlot.GetComponent<Image>();
                slotScript = this.feetSlot.GetComponent<EquippedSlot>();
                item = equippedGear.footGear;
                defaultResource = "feet";
                break;
            case Item.ItemType.Hands:
                slot = this.handsSlot.GetComponent<Image>();
                slotScript = this.handsSlot.GetComponent<EquippedSlot>();
                item = equippedGear.handGear;
                defaultResource = "hands";
                break;
            case Item.ItemType.Wings:
                slot = this.wingsSlot.GetComponent<Image>();
                slotScript = this.wingsSlot.GetComponent<EquippedSlot>();
                item = equippedGear.wingGear;
                defaultResource = "wings";
                break;
            default:
                throw new Exception("Invalid equipped item type for player: "
                    + itemType);
        }

        // If nothing is equipt, use the default resource
        if (item == null) {
            slot.sprite = Resources.Load<Sprite>("Inventory/" + defaultResource);
            return;
        }

        // Enforce the item type to be the type we are expecting and load it
        if (item.itemType == itemType) {
            slot.sprite = this.cache.LoadInventorySprite(item.resourceName);
            slotScript.item = item;
        } else {
            throw new Exception("Expected player gear of type: " + itemType
                + " but got gear of type: " + item.itemType);
        }
    }
}
