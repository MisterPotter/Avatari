using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * @author: Charlotte
 * @summary: Updates text showing the user's current level in header.
 */ 
public class LevelText : MonoBehaviour {

    private Text levelText;
    private Cache cache;

	void Update() {
        FindCache();
        FindText();
        UpdateText();
	}

    private void FindCache() {
        cache = Utility.LoadObject<Cache>("Cache");
    }

    private void FindText() {
        levelText = Utility.LoadObject<Text>("LevelText");
    }

    private void UpdateText() {
        int currLevel = cache.LoadPlayerLevel();
        levelText.text = "Level " + currLevel;
    }
}
