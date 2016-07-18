using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour, IPointerDownHandler {

    /*
     *  An empty slot has no current item it represents and will loose
     *  a lot of interactability. 
     */
    public bool empty { get { return item == null; } }
    public Image slot { get; set; }
    public Sprite slotDefault { get; set; }
    public Item item { get; set; }

    private Cache cache;
    private Transform dialogSpawner;
    private const string titleFormat = "Unequip {0}?";
    private const string statusFormat = "-{0} {1}";

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
            "Prefabs/UI/Inventory/Dialogs/UnequipDialog");
        if (dialogPrefab == null) {
            throw new Exception("Unequip dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateDialog(dialogPrefab);
    }

    private void CleanUpExistingDialogs() {
        foreach(Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text status = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(3).GetComponent<Button>();

        title.text = String.Format(titleFormat, this.item.itemName);
        status.text = String.Format(statusFormat, this.item.statBoost, this.item.statType.ToString());
        accept.onClick.AddListener(
            delegate {
                UnequipItem();
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

    /**
     *  Unequip an item from the slot itself. In order to do this, the sprite
     *  needs to be changes, the player object needs to be changed in the
     *  cache, and the reference to the item needs to be removed.
     */
    private void UnequipItem() {
        this.slot.sprite = this.slotDefault;
        this.cache.player.UnequipItem(this.item);
        this.item = null;
    }


}
