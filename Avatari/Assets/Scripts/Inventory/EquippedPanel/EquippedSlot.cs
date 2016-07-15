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
    public Item item { get; set; }

    private Transform dialogSpawner;
    private const string titleFormat = "Unequip {0}?";

    private void Awake() {
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(item == null) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Inventory/Dialogs/UnequipDialog");
        if (dialogPrefab == null) {
            throw new Exception("Unequip dialog prefab was not found.");
        }

        CreateDialog(dialogPrefab);
    }

    private void CreateDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(1).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(2).GetComponent<Button>();

        title.text = String.Format(titleFormat, this.item.itemName);
        accept.onClick.AddListener(delegate { print("Accept"); });
        decline.onClick.AddListener(
            delegate {
                Destroy(dialog);
            }
        );

        dialog.transform.SetParent(this.dialogSpawner, false);
    }


}
