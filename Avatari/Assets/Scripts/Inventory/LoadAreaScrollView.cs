using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/**
 *  @author: Tyler
 *  A class to load the inventory panel.
 */
public class LoadAreaScrollView : MonoBehaviour {

    private IDataSource cache;
    private GameObject rowPrefab;
    private Transform rowSpawner;
    private const float rowVertOffset = 220.0f;

    private void Awake() {
        Initialize();
        LoadAreas();
    }

    private void Initialize() {
        cache = Utility.LoadObject<IDataSource>("Cache");
        rowPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/AreaRow"
        );
        rowSpawner = Utility.LoadObject<Transform>("AreaRowSpawner");
    }

    private void LoadAreas() {
        List<Sprite> inventoryAreas = this.cache.LoadAreas();
        int rows = inventoryAreas.Count;
        for (int i = 0; i < rows; ++i) {
            int itemsPassed = i;
            IEnumerable<Sprite> areasLeft = inventoryAreas
                .Skip(itemsPassed)
                .Take(1);

            InstatiateRow(i, areasLeft);
        }
    }

    private void InstatiateRow(int row, IEnumerable<Sprite> sprites) {
        Vector3 offset = Vector3.down * rowVertOffset * row;
        GameObject clone = (GameObject)Instantiate(
            rowPrefab, offset, Quaternion.identity
        );
        clone.transform.SetParent(rowSpawner, false);

        int i = 0;
        foreach (Sprite sprite in sprites) {
            Image slot = clone.transform.GetChild(i++).GetChild(0)
                .GetComponent<Image>();
            slot.sprite = sprite;
        }
    }

}
