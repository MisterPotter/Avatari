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
    private Image headImage;
    private Image chestImage;
    private Image feetImage;
    private Image handsImage;
    private Image wingsImage;

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
        this.headImage = Utility.LoadObject<Image>("HeadSlot");
        this.chestImage = Utility.LoadObject<Image>("ChestSlot");
        this.feetImage = Utility.LoadObject<Image>("FeetSlot");
        this.handsImage = Utility.LoadObject<Image>("HandsSlot");
        this.wingsImage = Utility.LoadObject<Image>("WingsSlot");
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

        LoadEquippedSprite(Item.ItemType.Head, equippedGear);
        LoadEquippedSprite(Item.ItemType.Body, equippedGear);
        LoadEquippedSprite(Item.ItemType.Feet, equippedGear);
        LoadEquippedSprite(Item.ItemType.Hands, equippedGear);
        LoadEquippedSprite(Item.ItemType.Wings, equippedGear);
    }

    private void LoadEquippedSprite(Item.ItemType itemType, Player.EquippedGear equippedGear) {

        // Choose our slot to load into, the item we want to examine
        // and our default resource incase that item is null.
        Image slot;
        Item item;
        string defaultResource;
        switch(itemType) {
            case Item.ItemType.Head:
                slot = this.headImage;
                item = equippedGear.headGear;
                defaultResource = "head";
                break;
            case Item.ItemType.Body:
                slot = this.chestImage;
                item = equippedGear.chestGear;
                defaultResource = "chest";
                break;
            case Item.ItemType.Feet:
                slot = this.feetImage;
                item = equippedGear.footGear;
                defaultResource = "feet";
                break;
            case Item.ItemType.Hands:
                slot = this.handsImage;
                item = equippedGear.handGear;
                defaultResource = "hands";
                break;
            case Item.ItemType.Wings:
                slot = this.wingsImage;
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
        } else {
            throw new Exception("Expected player gear of type: " + itemType
                + " but got gear of type: " + item.itemType);
        }
    }
}
