using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour, IPointerDownHandler {

    /*
     *  An empty slot has no current item it represents and will loose
     *  a lot of interactability. 
     */
    public bool empty { get { return item == null; } }
    public Image slot { get; set; }
    public Item item { get; set; }

    private Cache cache;
    private Transform dialogSpawner;
    private const string TitleFormat = "Equip {0}?";

    private const string HeadSlotTag = "HeadSlot";
    private const string ChestSlotTag = "ChestSlot";
    private const string FeetSlotTag = "FeetSlot";
    private const string HandsSlotTag = "HandsSlot";
    private const string WingsSlotTag = "WingsSlot";

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    /**
     *  On tap down, create a dialog to remove items.
     */
    public void OnPointerDown(PointerEventData eventData) {
        if (empty) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/Dialogs/ItemDialog");
        if (dialogPrefab == null) {
            throw new Exception("Unequip dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        if(this.item.IsEquippable()) {
            CreateItemDialog(dialogPrefab);
        }
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateItemDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(3).GetComponent<Button>();

        title.text = String.Format(TitleFormat, this.item.itemName);
        description.text = item.itemDescription;
        accept.onClick.AddListener(
            delegate {
                EquipItem();
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
     *  Equip an item to the player, 
     */
    private void EquipItem() {
        Sprite resource = this.cache.LoadInventorySprite(item.resourceName);
        GameObject slot;
        switch (item.itemType) {
            case Item.ItemType.Head:
                slot = GameObject.FindGameObjectWithTag(HeadSlotTag);
                slot.GetComponent<Image>().sprite = resource;
                slot.GetComponent<EquippedSlot>().item = this.item;
                this.cache.player.gear.headGear = this.item;
                break;
            case Item.ItemType.Body:
                slot = GameObject.FindGameObjectWithTag(ChestSlotTag);
                slot.GetComponent<Image>().sprite = resource;
                slot.GetComponent<EquippedSlot>().item = this.item;
                this.cache.player.gear.chestGear = this.item;
                break;
            case Item.ItemType.Feet:
                slot = GameObject.FindGameObjectWithTag(FeetSlotTag);
                slot.GetComponent<Image>().sprite = resource;
                slot.GetComponent<EquippedSlot>().item = this.item;
                this.cache.player.gear.footGear = this.item;
                break;
            case Item.ItemType.Hands:
                slot = GameObject.FindGameObjectWithTag(HandsSlotTag);
                slot.GetComponent<Image>().sprite = resource;
                slot.GetComponent<EquippedSlot>().item = this.item;
                this.cache.player.gear.handGear = this.item;
                break;
            case Item.ItemType.Wings:
                slot = GameObject.FindGameObjectWithTag(WingsSlotTag);
                slot.GetComponent<Image>().sprite = resource;
                slot.GetComponent<EquippedSlot>().item = this.item;
                this.cache.player.gear.wingGear = this.item;
                break;
            default:
                Debug.LogError("Invalid equipped item type for player: "
                    + this.item.itemType);
                return;
        }
    }



}
