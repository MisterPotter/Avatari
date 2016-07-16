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

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    /**
     *  On tap down, create a dialog to remove items.
     */
    public void OnPointerDown(PointerEventData eventData) {
        if(item == null) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/Dialogs/UnequipDialog");
        if (dialogPrefab == null) {
            throw new Exception("Unequip dialog prefab was not found.");
        }

        CreateDialog(dialogPrefab);
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(1).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(2).GetComponent<Button>();

        title.text = String.Format(titleFormat, this.item.itemName);
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
