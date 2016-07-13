using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/**
 *  @author: Tyler
 *  A class to load the inventory panel.
 */
public class LoadInventoryScrollView : MonoBehaviour {

    private Cache cache;
    private GameObject rowPrefab;
    private Transform rowSpawner;
    private const int itemsPerRow = 5;
    private const float rowVertOffset = 60.0f;

    private void Awake() {
        Initialize();
        LoadInventory();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        rowPrefab= Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/InventoryRow"
        );
        rowSpawner = Utility.LoadObject<Transform>("InventoryRowSpawner");

    }

    private void LoadInventory() {
        List<Item> inventoryItems = this.cache.LoadItems();
        int rows = (inventoryItems.Count / itemsPerRow) + 1;
        for(int i=0; i<rows; ++i) {
            int itemsPassed = i * itemsPerRow;
            int numItemsLeft = Mathf.Min(inventoryItems.Count-itemsPassed,
                itemsPerRow
            );
            IEnumerable<Item> itemsLeft = inventoryItems
                .Skip(itemsPassed)
                .Take(numItemsLeft);

            InstatiateRow(i, itemsLeft);
        }
    }

    private void InstatiateRow(int row, IEnumerable<Item> items) {
        Vector3 offset = Vector3.down * rowVertOffset * row;
        GameObject clone = (GameObject)Instantiate(
            rowPrefab, offset, Quaternion.identity
        );
        clone.transform.SetParent(rowSpawner, false);

        int i = 0;
        foreach (Item item in items) {
            Image slot = clone.transform.GetChild(i++).GetChild(0)
                .GetComponent<Image>();
            slot.sprite = this.cache.LoadInventorySprite(item.resourceName);
        }
    }

}
