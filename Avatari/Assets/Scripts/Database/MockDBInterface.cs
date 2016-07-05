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
    }

    private void PopulatePlayerInfo() {
        Cache cache = Utility.LoadObject<Cache>("Cache");
        Player player = new Player();
        player.gear = new Player.EquippedGear(
            new Item("Helmet", "iron_helm", "", 1, Item.ItemType.Head),
            new Item("Body", "iron_chestplate", "", 2, Item.ItemType.Body),
            new Item("Feet", "iron_boots", "", 3, Item.ItemType.Feet),
            new Item("Hands", "iron_gloves", "", 4, Item.ItemType.Hands),
            new Item("Wings", "wings", "", 5, Item.ItemType.Wings)
        );
        cache.player = player;
    }

    private void PopulateBosses() {

    }

}
