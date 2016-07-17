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
    private RectTransform content;
    private GameObject rowPrefab;
    private Transform rowSpawner;
    private const int itemsPerRow = 5;
    private const float rowVertOffset = 60.0f;
    private const float bottomPadding = 10.0f;

    private void Awake() {
        Initialize();
        LoadInventory();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        content = Utility.LoadObject<RectTransform>("ItemContent");
        rowPrefab= Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/InventoryRow"
        );
        rowSpawner = Utility.LoadObject<Transform>("InventoryRowSpawner");

    }

    private void LoadInventory() {
        List<Item> inventoryItems = this.cache.LoadItems();
        int rows = (inventoryItems.Count / itemsPerRow) + 1;
        content.sizeDelta = new Vector2(0.0f, rows*rowVertOffset+bottomPadding);
        for (int i=0; i<rows; ++i) {
            int itemsPassed = i * itemsPerRow;
            int numItemsLeft = Mathf.Min(inventoryItems.Count-itemsPassed,
                itemsPerRow
            );
            IEnumerable<Item> itemsLeft = inventoryItems
                .Skip(itemsPassed)
                .Take(numItemsLeft);

            InstantiateRow(i, itemsLeft);
        }
    }

    private void InstantiateRow(int row, IEnumerable<Item> items) {
        Vector3 offset = Vector3.down * rowVertOffset * row;
        GameObject clone = (GameObject)Instantiate(
            rowPrefab, offset, Quaternion.identity
        );
        clone.transform.SetParent(rowSpawner, false);

        int i = 0;
        foreach (Item item in items) {
            Transform slot = clone.transform.GetChild(i++).GetChild(0);
            Image slotImage = slot.GetComponent<Image>();
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();

            slotImage.sprite = this.cache.LoadInventorySprite(item.resourceName);
            slotScript.item = item;
            slotScript.slot = slotImage;
        }
    }

}
