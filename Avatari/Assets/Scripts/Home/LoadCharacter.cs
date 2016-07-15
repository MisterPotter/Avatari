using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/**
 * @author: Charlotte
 * @summary: Loads character sprite and character name on home screen
 */
public class LoadCharacter : MonoBehaviour {

    private Text characterName;
    private Image characterImage;

    private Cache cache;

    void Awake() {
        FindCache();
        FindCharacterSlots();
        UpdateCharacterImage();
        UpdateCharacterText();
    }

    private void FindCache() {
        cache = Utility.LoadObject<Cache>("Cache");
    }

    private void FindCharacterSlots() {
        characterName = Utility.LoadObject<Text>("CharacterText");
        characterImage = Utility.LoadObject<Image>("CharacterSlot");
    }

    private void UpdateCharacterImage() {
        string spriteName = this.cache.LoadCharacterSprite();
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Characters/" + spriteName);
        if (spriteSheet.Length == 0) {
            throw new Exception("Character sprite: " + spriteName +
                " could not be found.");
        }
        characterImage.sprite = Utility.GetSprite("idle", spriteSheet);
    }

    private void UpdateCharacterText() {
        characterName.text = this.cache.LoadCharacterSprite();
    }
}
