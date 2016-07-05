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
     *  All information to do with the player.
     */
    private Player _player;
    public Player player {
        get { return _player; }
        set { _player = value; }
    }

    public List<Item> LoadItems() {
        return inventoryItems;
    }

    public Player.EquippedGear LoadEquippedItems() {
        return player.gear;
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
    }
}
