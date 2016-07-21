using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CharacterSlot : MonoBehaviour, IPointerDownHandler {

    /*
     *  An empty slot has no current item it represents and will loose
     *  a lot of interactability. 
     */
    public bool empty { get { return tari == null; } }
    public Tari tari;

    private Cache cache;
    private Transform dialogSpawner;
    private const string TitleFormat = "Change character to {0}?";

    private Image characterImage;

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
        characterImage = Utility.LoadObject<Image>("CharacterSlot");
    }

    /**
     *  On tap down, create a dialog to remove items.
     */
    public void OnPointerDown(PointerEventData eventData) {
        if (empty) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Dialogs/CharacterDialog");
        if (dialogPrefab == null) {
            throw new Exception("Character dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateCharacterDialog(dialogPrefab);
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    /**
     *  Create a dialog that is capable of unequipping an item.
     */
    private void CreateCharacterDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(3).GetComponent<Button>();

        title.text = String.Format(TitleFormat, this.tari.name);
        description.text = this.tari.description;
        accept.onClick.AddListener(
            delegate {
                EquipSprite();
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

    public void EquipSprite() {
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Characters/" + tari.spriteName);
        this.characterImage.sprite = Utility.GetSprite("idle", spriteSheet);
        this.cache.player.sprite = this.tari.spriteName;
    }
}
