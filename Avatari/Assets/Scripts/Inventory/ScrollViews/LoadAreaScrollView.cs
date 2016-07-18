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
    private RectTransform content;
    private Transform rowSpawner;
    private const float rowVertOffset = 220.0f;
    private const float bottomPadding = 20.0f;

    private void Awake() {
        Initialize();
        LoadAreas();
    }

    private void Initialize() {
        cache = Utility.LoadObject<IDataSource>("Cache");
        rowPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/AreaRow"
        );
        content = Utility.LoadObject<RectTransform>("AreaContent");
        rowSpawner = Utility.LoadObject<Transform>("AreaRowSpawner");
    }

    private void LoadAreas() {
        List<Area> inventoryAreas = this.cache.LoadAreas();
        int rows = inventoryAreas.Count;

        // Set the size of the content to hold the content
        content.sizeDelta = new Vector2(0.0f, rowVertOffset*rows+bottomPadding);

        for (int i = 0; i < rows; ++i) {
            InstatiateRow(i, inventoryAreas[i]);
        }
    }

    private void InstatiateRow(int row, Area area) {
        Vector3 offset = Vector3.down * rowVertOffset * row;
        GameObject clone = (GameObject)Instantiate(
            rowPrefab, offset, Quaternion.identity
        );
        clone.transform.SetParent(rowSpawner, false);

        Sprite sprite = Resources.Load<Sprite>("Sprites/Areas/" + area.spriteName);
        if(sprite == null) {
            Debug.LogError("Error: couldn't load area sprite: " + area.spriteName);
        }
        Transform slotObject = clone.transform.GetChild(0).GetChild(0);
        Image slot = slotObject.GetComponent<Image>();
        AreaSlot slotScript = slotObject.GetComponent<AreaSlot>();
        slot.sprite = sprite;
        slotScript.area = area;
    }

}
