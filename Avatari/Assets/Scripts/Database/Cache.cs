using UnityEngine;
using System.Collections.Generic;
using System;

/**
 *  The cache class to be used throughout all scenes.
 */
public class Cache : MonoBehaviour, IDataSource {
    public static Cache cache;

    /**
     *  The list of inventory areas that the player owns.
     */
    private List<Sprite> inventoryAreas;

    /**
     *  The list of inventory characters that the player owns.
     */
    private List<Sprite> inventoryCharacters;

    /**
     *  The list of inventory items that the player owns.
     */
    private List<Item> inventoryItems;

    /**
     *  The list of bosses that will populate boss panels on 'Bosses' scene.
     */
    private List<Boss> bosses;

    /**
    *  The list of a user's daily goals.
    */
    public DailyGoals dailyGoals { get; set; }

    /**
    *  The list of a user's daily goals.
    */
    public Challenges challenges { get; set; }

    /**
    *  The list of a user's daily goals.
    */
    public LifetimeGoals lifetimeGoals { get; set; }

    /**
     *  All inventory icons.
     */
    private Sprite[] inventorySprites;

    /**
     *  All boss icons
     */
    private Sprite[] bossSprites;

    /**
     *  All utility icons.
     */
    private Sprite[] utilitySprites;

    /**
     *  All information to do with the player.
     */
    public Player player { get; set; }

    /**
     *  Retrieval functions
     */
    public List<Sprite> LoadAreas() {
        return cache.inventoryAreas;
    }

    public List<Boss> LoadBosses() {
        return cache.bosses;
    }

    public List<Sprite> LoadCharacters() {
        return cache.inventoryCharacters;
    }

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

    public Sprite LoadBossSprite(string spriteName) {
        return Utility.GetSprite(spriteName, this.bossSprites);
    }

    public Sprite LoadUtilitySprite(string spriteName) {
        return Utility.GetSprite(spriteName, this.utilitySprites);
    }

    public int LoadPlayerLevel() {
        return cache.player.stats.level.CurrentValue;
    }

    /**
     *  Used to add items to the inventory list.
     */
    public void AddItemToInventory(Item item) {
        cache.inventoryItems.Add(item);
    }

    /**
     *  Used to add characters to the characters list.
     */
    public void AddCharacterToInventory(string name) {
        Sprite[] characterSpriteSheet = Resources.LoadAll<Sprite>(
            "Characters/" + name);
        cache.inventoryCharacters.Add(Utility.GetSprite(
            "idle", characterSpriteSheet));
    }

    public void AddAreaToInventory(string name) {
        Sprite area = Resources.Load<Sprite>("Sprites/Areas/" + name);
        if(area != null) {
            cache.inventoryAreas.Add(area);
        }
    }

    /**
    *  Used to add bosses to the list.
    */
    public void AddBossToList(Boss boss) {
        cache.bosses.Add(boss);
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
        cache.inventoryCharacters = new List<Sprite>();
        cache.inventoryAreas = new List<Sprite>();
        cache.player = new Player();
        cache.bosses = new List<Boss>();
        cache.dailyGoals = new DailyGoals();
        cache.challenges = new Challenges();
        cache.lifetimeGoals = new LifetimeGoals();
        LoadSprites();
    }

    private void LoadSprites() {
        Sprite[] inventorySprites = Resources.LoadAll<Sprite>("Inventory/inventory_icons");
        Sprite[] bossSprites = Resources.LoadAll<Sprite>("Characters/PrototypeBosses");
        Sprite[] utilitySprites = Resources.LoadAll<Sprite>("Sprites/Utility");
        if (inventorySprites.Length == 0 || bossSprites.Length == 0) {
            throw new Exception("Inventory sprites could not be loaded.");
        }
        cache.inventorySprites = inventorySprites;
        cache.bossSprites = bossSprites;
        cache.utilitySprites = utilitySprites;
    }
}
