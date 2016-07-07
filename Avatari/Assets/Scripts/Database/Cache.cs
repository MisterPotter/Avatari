using UnityEngine;
using System.Collections.Generic;
using System;

/**
 *  The cache class to be used throughout all scenes.
 */
public class Cache : MonoBehaviour, IDataSource {
    public static Cache cache;

    /**
     *  The list of inventory items that the player owns.
     */
    private List<Item> inventoryItems;

    /**
     *  All inventory icons.
     */
    private Sprite[] inventorySprites;

    /**
     *  All information to do with the player.
     */
    public Player player { get; set; }

    public List<Item> LoadItems() {
        return cache.inventoryItems;
    }

    public Player.EquippedGear LoadEquippedItems() {
        return cache.player.gear;
    }

    public string LoadCharacterSprite() {
        return cache.player.sprite;
    }

    public Sprite LoadInventorySprite(string spriteName) {
        return Utility.GetSprite(spriteName, this.inventorySprites);
    }

    /**
     *  Used to add items to the inventory list.
     */
    public void AddItemToInventory(Item item) {
        cache.inventoryItems.Add(item);
    }

    private void Awake() {
        if(cache == null) {
            CreateInstance();
        } else if(cache != this) {
            Destroy(gameObject);
        }
    }

    private void CreateInstance() {
        DontDestroyOnLoad(gameObject);
        cache = this;
        cache.inventoryItems = new List<Item>();
        cache.player = new Player();
        cache.inventorySprites = LoadSprites();
    }

    private Sprite[] LoadSprites() {
        Sprite[] inventorySprites = Resources.LoadAll<Sprite>("Inventory/inventory_icons");
        if (inventorySprites.Length == 0) {
            throw new Exception("Inventory sprites could not be loaded.");
        }

        return inventorySprites;
    }
}
