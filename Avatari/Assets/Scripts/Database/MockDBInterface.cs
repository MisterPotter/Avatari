using UnityEngine;

public class MockDBInterface : MonoBehaviour {
    Cache cache;

    private void Awake() {
        PopulateCache();
    }

    private void PopulateCache() {
        LoadCache();
        PopulateInventory();
        PopulatePlayerInfo();
        PopulateGoals();
        PopulateBosses();
    }

    private void LoadCache() {
        this.cache = Utility.LoadObject<Cache>("Cache"); 
    }

    private void PopulateInventory() {
        // Add inventory objects
        //cache.AddItemToInventory(new Item("Feather", "feather", "A common feather.", 8, Item.ItemType.Other, Item.ItemRarity.Common));
        //cache.AddItemToInventory(new Item("Spear", "spear", "A spear used for fighting.", 9, Item.ItemType.Hands, Item.ItemRarity.Common));
        //cache.AddItemToInventory(new Item("Crystal Bow", "crystal_bow", "A bow that uses MP for ammunition. Retrieved from gnome village", 10, Item.ItemType.Hands, Item.ItemRarity.Rare));*/

        // Add character objects
        cache.AddCharacterToInventory(new Tari(0, "Vivi", "Vivi", "A mystical creature."));
        cache.AddCharacterToInventory(new Tari(1, "Donkey Kong", "DonkeyKong", "Load up those barrels."));
        cache.AddCharacterToInventory(new Tari(2, "Mega Dude", "MegaDude", "Not quite net worthy."));

        // Add areas
        cache.AddAreaToInventory("Lava");
        cache.AddAreaToInventory("Forest");
        cache.AddAreaToInventory("Final");
    }

    private void PopulatePlayerInfo() {
        Player player = new Player();
        player.gear = new Player.EquippedGear(
            new Item("Iron Helmet", "iron_helm", "An iron bucket for your head.", 1, Item.ItemType.Head, Item.ItemRarity.Common, Statistic.Type.Defense, 2),
            new Item("Iron Body", "iron_chestplate", "An iron chestplate, will provide some defense.", 2, Item.ItemType.Body, Item.ItemRarity.Common, Statistic.Type.Agility, 1),
            new Item("Iron Boots", "iron_boots", "Iron booties.", 3, Item.ItemType.Feet, Item.ItemRarity.Common, Statistic.Type.Defense, 3),
            new Item("Iron Gloves", "iron_gloves", "Iron Mits.", 4, Item.ItemType.Hands, Item.ItemRarity.Common, Statistic.Type.Strength, 2),
            new Item("Mystic Wings", "wings", "I wonder how these work?", 5, Item.ItemType.Wings, Item.ItemRarity.Common, Statistic.Type.Agility, 1)
        );
        player.sprite = "Vivi";
        player.stats = new PlayerStatistic(4, 4100, 2, 3, 4, 5);
        cache.player = player;
    }

    private void PopulateGoals() {
        cache.dailyGoals = new DailyGoals(2000, 200, 2, 30);
        cache.dailyGoals.stepGoal.progress = 1256;
        cache.dailyGoals.calorieGoal.progress = 112;
        cache.dailyGoals.distanceGoal.progress = 1.22f;
        cache.dailyGoals.activeMinGoal.progress = 26;

        cache.challenges = new Challenges(20, 10, 10, 5);
        cache.challenges.biking.progress = 10.6f;
        cache.challenges.running.progress = 4.6f;
        cache.challenges.hiking.progress = 0.6f;
        cache.challenges.swimming.progress = 0.5f;

        cache.lifetimeGoals = new LifetimeGoals(500000, 10000, 500, 6000);
        cache.lifetimeGoals.stepGoal.progress = 1256;
        cache.lifetimeGoals.calorieGoal.progress = 112;
        cache.lifetimeGoals.distanceGoal.progress = 1.22f;
        cache.lifetimeGoals.activeMinGoal.progress = 26;
    }

    private void PopulateBosses() {
        cache.AddBossToList(new Boss("Vicky", "", 2, 1));
        cache.AddBossToList(new Boss("Rombo", "", 3, 2));
        cache.AddBossToList(new Boss("Patsy", "", 5, 3));
        cache.AddBossToList(new Boss("Squidward", "", 8, 4));
        cache.AddBossToList(new Boss("Bloop", "", 10, 5));
        cache.AddBossToList(new Boss("Ali", "", 12, 6));
        cache.AddBossToList(new Boss("Moeby", "", 15, 7));
    }

}
