using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class AreaSlot : MonoBehaviour, IPointerDownHandler {

    /*
        *  An empty slot has no current item it represents and will loose
        *  a lot of interactability. 
        */
    public bool empty { get { return area == null; } }
    public Area area;

    private Cache cache;
    private Transform dialogSpawner;
    private const string TitleFormat = "Change Area to {0}?";

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    /**
     *  On tap down, create a dialog to remove items.
     */
    public void OnPointerDown(PointerEventData eventData) {
        if(empty) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/Dialogs/AreaDialog");
        if (dialogPrefab == null) {
            throw new Exception("Area dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateAreaDialog(dialogPrefab);
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateAreaDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(3).GetComponent<Button>();

        title.text = String.Format(TitleFormat, this.area.name);
        description.text = this.area.description;
        accept.onClick.AddListener(
            delegate {
                this.cache.player.area = this.area;
                Destroy(dialog);
            }
        );
        decline.onClick.AddListener(
            delegate {
                Destroy(dialog);
            }
        );

        dialog.transform.SetParent(this.dialogSpawner, false);
    }
}
