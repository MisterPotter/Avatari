using UnityEngine;

public class MockDBInterface : MonoBehaviour {

    private void Awake() {
        PopulateCache();
    }

    private void PopulateCache() {
        PopulateInventory();
        PopulatePlayerInfo();
        PopulateBosses();
    }

    private void PopulateInventory() {
        // Add inventory objects
        Cache cache = Utility.LoadObject<Cache>("Cache");
        cache.AddItemToInventory(new Item("Ruby Amulet", "ruby_amulet", "A gem commonly found in, Burma. Now made into a necklase.", 6, Item.ItemType.Neck, Item.ItemRarity.Uncommon));
        cache.AddItemToInventory(new Item("Iron Ring", "iron_ring", "Looks like graduation is here.", 7, Item.ItemType.Neck, Item.ItemRarity.Uncommon));
        cache.AddItemToInventory(new Item("Feather", "feather", "A common feather.", 8, Item.ItemType.Neck, Item.ItemRarity.Common));

        // Add character objects
        cache.AddCharacterToInventory("Vivi");
        cache.AddCharacterToInventory("DonkeyKong");
        cache.AddCharacterToInventory("MegaDude");

        // Add areas
        cache.AddAreaToInventory("Lava");
        cache.AddAreaToInventory("Forest");
        cache.AddAreaToInventory("Final");
    }

    private void PopulatePlayerInfo() {
        Cache cache = Utility.LoadObject<Cache>("Cache");
        Player player = new Player();
        player.gear = new Player.EquippedGear(
            new Item("Helmet", "iron_helm", "An iron bucket for your head.", 1, Item.ItemType.Head, Item.ItemRarity.Common),
            new Item("Body", "iron_chestplate", "An iron chestplate, will provide some defense.", 2, Item.ItemType.Body, Item.ItemRarity.Common),
            new Item("Feet", "iron_boots", "Iron booties.", 3, Item.ItemType.Feet, Item.ItemRarity.Common),
            new Item("Hands", "iron_gloves", "Iron Mits.", 4, Item.ItemType.Hands, Item.ItemRarity.Common),
            new Item("Wings", "wings", "I wonder how these work?", 5, Item.ItemType.Wings, Item.ItemRarity.Common)
        );
        player.sprite = "Vivi";
        cache.player = player;
    }

    private void PopulateBosses() {
        Cache cache = Utility.LoadObject<Cache>("Cache");
        cache.AddBossToList(new Boss("Vicky", "", 2, 1));
        cache.AddBossToList(new Boss("Rombo", "", 3, 2));
        cache.AddBossToList(new Boss("Patsy", "", 5, 3));
        cache.AddBossToList(new Boss("Squidward", "", 8, 4));
        cache.AddBossToList(new Boss("Bloop", "", 10, 5));
        cache.AddBossToList(new Boss("Ali", "", 12, 6));
        cache.AddBossToList(new Boss("Moeby", "", 15, 7));
    }

}
