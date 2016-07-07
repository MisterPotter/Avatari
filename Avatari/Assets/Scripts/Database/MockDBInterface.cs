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
        Cache cache = Utility.LoadObject<Cache>("Cache");
        cache.AddItemToInventory(new Item("Ruby Amulet", "ruby_amulet", "", 6, Item.ItemType.Neck, Item.ItemRarity.Uncommon));
        cache.AddItemToInventory(new Item("Iron Ring", "iron_ring", "", 7, Item.ItemType.Neck, Item.ItemRarity.Common));
        cache.AddItemToInventory(new Item("Feather", "feather", "A common feather.", 8, Item.ItemType.Neck, Item.ItemRarity.Common));
    }

    private void PopulatePlayerInfo() {
        Cache cache = Utility.LoadObject<Cache>("Cache");
        Player player = new Player();
        player.gear = new Player.EquippedGear(
            new Item("Helmet", "iron_helm", "", 1, Item.ItemType.Head, Item.ItemRarity.Common),
            new Item("Body", "iron_chestplate", "", 2, Item.ItemType.Body, Item.ItemRarity.Common),
            new Item("Feet", "iron_boots", "", 3, Item.ItemType.Feet, Item.ItemRarity.Common),
            new Item("Hands", "iron_gloves", "", 4, Item.ItemType.Hands, Item.ItemRarity.Common),
            new Item("Wings", "wings", "", 5, Item.ItemType.Wings, Item.ItemRarity.Common)
        );
        player.sprite = "DonkeyKong";
        cache.player = player;
    }

    private void PopulateBosses() {

    }

}
