using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 *  @author: Tyler
 *
 *  This class represent one equipped slots in which a character can
 *  have an item equipped to.
 */
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

    /*
     *  Constants.
     */
    public const string CharacterSlot = "CharacterSlot";
    public const string HeadSlot = "HeadSlot";
    public const string BodySlot = "BodySlot";
    public const string FeetSlot = "FeetSlot";
    public const string WeaponSlot = "WeaponSlot";
    public const string NecklaceSlot = "NecklaceSlot";
    public const string WingsSlot = "WingsSlot";
    public const string RingsSlot = "RingSlot";
    public const string ShieldSlot = "ShieldSlot";

    private const string TitleFormat = "Unequip {0}?";
    private const string StatusFormat = "-{0} {1}";
    private const string UnequipDialog = "Prefabs/UI/Dialogs/UnequipDialog";

    private void Awake() {
        cache = Utility.LoadObject<Cache>(Cache.Tag);
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    /**
     *  On tap down, create a dialog to remove items.
     */
    public void OnPointerDown(PointerEventData eventData) {
        if(empty) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(UnequipDialog);
        if (dialogPrefab == null) {
            throw new Exception("Unequip dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateUnequipDialog(dialogPrefab);
    }

    /**
     *  Remove all dialogs under a given dialog spawner.
     */
    private void CleanUpExistingDialogs() {
        foreach(Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateUnequipDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text status = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(3).GetComponent<Button>();

        title.text = String.Format(TitleFormat, this.item.itemName);
        status.text = String.Format(StatusFormat, this.item.statBoost, this.item.statType.ToString());
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
