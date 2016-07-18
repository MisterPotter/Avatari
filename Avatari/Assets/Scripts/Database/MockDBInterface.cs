using UnityEngine;

public class MockDBInterface : MonoBehaviour {
    Cache cache;

    private void Awake() {
        PopulateCache();
    }

    private void PopulateCache() {
        LoadCache();
        PopulateInventory();
        //PopulatePlayerInfo();
        PopulateGoals();
        PopulateBosses();
    }

    private void LoadCache() {
        this.cache = Utility.LoadObject<Cache>("Cache"); 
    }

    private void PopulateInventory() {
        // Add inventory objects
        Cache cache = Utility.LoadObject<Cache>("Cache");
        cache.AddItemToInventory(new Item(
            "Ruby Amulet", "ruby_amulet",
            "A gem commonly found in, Burma. Now made into a necklase.",
            6, Item.ItemType.Neck, Item.ItemRarity.Uncommon,
            Statistic.Type.Agility, 2
        ));
        cache.AddItemToInventory(new Item(
            "Iron Ring", "iron_ring", "Looks like graduation is here.",
            7, Item.ItemType.Ring, Item.ItemRarity.Uncommon,
            Statistic.Type.Defense, 1
        ));
        cache.AddItemToInventory(new Item(
            "Spear", "spear", "A spear used for fighting.", 9,
            Item.ItemType.Weapon, Item.ItemRarity.Common,
            Statistic.Type.Strength, 3
        ));
        cache.AddItemToInventory(new Item(
            "Crystal Bow", "crystal_bow",
            "A bow that uses MP for ammunition. Retrieved from gnome village",
            10, Item.ItemType.Weapon, Item.ItemRarity.Rare,
            Statistic.Type.Strength, 5
        ));
        cache.AddItemToInventory(new Item(
            "Wooden Round Shield", "wooden_round_shield",
            "Let's get that parry hype going!",
            6, Item.ItemType.Shield, Item.ItemRarity.Common,
            Statistic.Type.Defense, 3
        ));

        // Add character objects
        /*cache.AddCharacterToInventory(new Tari(0, "Vivi", "Vivi", "A mystical creature."));
        cache.AddCharacterToInventory(new Tari(1, "Donkey Kong", "DonkeyKong", "Load up those barrels."));
        cache.AddCharacterToInventory(new Tari(2, "Mega Dude", "MegaDude", "Not quite net worthy."));*/

        // Add areas
        /*cache.AddAreaToInventory(new Area(1, "Lava", "Lava", "Lava like, we should destroy that ring here."));
        cache.AddAreaToInventory(new Area(2, "Forest", "Forest", "Nature like."));
        cache.AddAreaToInventory(new Area(3, "Final", "Final", "Is this out of Naruto?"));*/
    }

    private void PopulatePlayerInfo() {
        Player player = new Player();
        player.gear = new Player.EquippedGear(
            new Item("Iron Helmet", "iron_helm", "An iron bucket for your head.", 1, Item.ItemType.Head, Item.ItemRarity.Common, Statistic.Type.Defense, 2),
            new Item("Iron Body", "iron_chestplate", "An iron chestplate, will provide some defense.", 2, Item.ItemType.Body, Item.ItemRarity.Common, Statistic.Type.Agility, 1),
            new Item("Iron Boots", "iron_boots", "Iron booties.", 3, Item.ItemType.Feet, Item.ItemRarity.Common, Statistic.Type.Defense, 3),
            new Item("Iron Gloves", "iron_gloves", "Iron Mits.", 4, Item.ItemType.Weapon, Item.ItemRarity.Common, Statistic.Type.Defense, 2),
            null,
            new Item("Mystic Wings", "wings", "I wonder how these work?", 5, Item.ItemType.Wings, Item.ItemRarity.Common, Statistic.Type.Agility, 1),
            null,
            null
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
        cache.AddBossToList(new Boss("Vicky", "", new PlayerStatistic(2, 2500, 13, 1, 1, 1), 1));
        cache.AddBossToList(new Boss("Rombo", "", new PlayerStatistic(3, 3100, 14, 2, 3, 2 ), 2));
        cache.AddBossToList(new Boss("Patsy", "", new PlayerStatistic(5, 5500, 14, 3, 3, 5 ), 3));
        cache.AddBossToList(new Boss("Squidward", "", new PlayerStatistic(8, 8800, 15, 2, 4, 6), 4));
        cache.AddBossToList(new Boss("Bloop", "", new PlayerStatistic(10, 10300, 16, 4, 5, 7 ), 5));
        cache.AddBossToList(new Boss("Ali", "", new PlayerStatistic(12, 12600, 17, 8, 9, 12), 6));
        cache.AddBossToList(new Boss("Moeby", "", new PlayerStatistic(15, 15010, 20, 13, 15, 18), 7));
    }

}
