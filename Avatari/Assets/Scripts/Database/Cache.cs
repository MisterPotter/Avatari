using UnityEngine;
using System.Collections.Generic;
using System;

/**
 *  The cache class to be used throughout all scenes.
 */
public class Cache : MonoBehaviour, IDataSource {

    /**
     *  Singleton instance.
     */
    public static Cache cache;

    public Fitbit fitbit { get; set; }

    /**
     *  The list of inventory areas that the player owns.
     */
    private List<Area> inventoryAreas;

    /**
     *  The list of inventory characters that the player owns.
     */
    private List<Tari> inventoryCharacters;

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
     *  The boss for the battle.
     */
    public Boss boss { get; set; }
    
    /*
     *  The session key to use for all requests.
     */
    public int sessionKey { get; set; }

    /**
    *  Retrieval functions
    */
    public List<Area> LoadAreas() {
        return cache.inventoryAreas;
    }

    public List<Boss> LoadBosses() {
        return cache.bosses;
    }

    public List<Tari> LoadCharacters() {
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
     *  Fitbit data
     */

    public void AddPairToCalories(FitbitPair<int> pair) {
        this.fitbit.calories.Add(pair);
    }

    public void AddPairToActivityCalories(FitbitPair<int> pair) {
        this.fitbit.activityCalories.Add(pair);
    }

    public void AddPairToSteps(FitbitPair<int> pair) {
        this.fitbit.activeSteps.Add(pair);
    }

    public void AddPairToDistance(FitbitPair<double> pair) {
        this.fitbit.activeDistance.Add(pair);
    }

    public void AddPairToFairlyActive(FitbitPair<int> pair) {
        this.fitbit.fairlyActive.Add(pair);
    }

    public void AddPairToVeryActive(FitbitPair<int> pair) {
        this.fitbit.veryActive.Add(pair);
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
    public void AddCharacterToInventory(Tari tari) {
        cache.inventoryCharacters.Add(tari);
    }

    /**
     *  Used to add areas to inventory.
     */
    public void AddAreaToInventory(Area area) {
        cache.inventoryAreas.Add(area);
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
        fitbit = new Fitbit();
        cache.inventoryItems = new List<Item>();
        cache.inventoryCharacters = new List<Tari>();
        cache.inventoryAreas = new List<Area>();
        cache.player = new Player();
        cache.bosses = new List<Boss>();
        cache.dailyGoals = new DailyGoals();
        cache.challenges = new Challenges();
        cache.lifetimeGoals = new LifetimeGoals();
        LoadDefaultGoals();
        LoadDefaultBosses();
        LoadSprites();
    }

    /**
     * Probably won't end up getting the user's personal goals. 
     * Will populate default ones in the cache instance.
     */ 
    private void LoadDefaultGoals() {
        cache.dailyGoals = new DailyGoals(Constants.StepGoal,
                                          Constants.CalorieGoal,
                                          Constants.DistanceGoal,
                                          Constants.ActiveMinutesGoal);

        cache.challenges = new Challenges(Constants.BikeChallenge,
                                          Constants.RunningChallenge,
                                          Constants.HikingChallenge);

        cache.lifetimeGoals = new LifetimeGoals(Constants.LifeStepGoal,
                                               // Constants.LifeCalorieGoal,
                                                Constants.LifeDistanceGoal,
                                                Constants.LifeActiveMinutesGoal);
    }

    private void LoadDefaultBosses() {
        cache.AddBossToList(new Boss("Vicky", "", new PlayerStatistic(1, 1500, 13, 1, 1, 1), 1));
        cache.AddBossToList(new Boss("Rombo", "", new PlayerStatistic(3, 3100, 14, 2, 3, 2), 2));
        cache.AddBossToList(new Boss("Patsy", "", new PlayerStatistic(5, 5500, 14, 3, 3, 5), 3));
        cache.AddBossToList(new Boss("Squidward", "", new PlayerStatistic(8, 8800, 15, 2, 4, 6), 4));
        cache.AddBossToList(new Boss("Bloop", "", new PlayerStatistic(10, 10300, 16, 4, 5, 7), 5));
        cache.AddBossToList(new Boss("Ali", "", new PlayerStatistic(12, 12600, 17, 8, 9, 12), 6));
        cache.AddBossToList(new Boss("Moeby", "", new PlayerStatistic(15, 15010, 20, 13, 15, 18), 7));
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
