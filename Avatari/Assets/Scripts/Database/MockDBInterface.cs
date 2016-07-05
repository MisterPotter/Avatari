using UnityEngine;

public class MockDBInterface : MonoBehaviour {

    private void Awake() {
        PopulateCache();
    }

    private void PopulateCache() {
        PopulateInventory();
        PopulateBosses();
    }

    private void PopulateInventory() {
        PlayerPrefs.SetString("Helm", "helm");
        PlayerPrefs.SetString("Chest", "chest");
        PlayerPrefs.SetString("Hands", "hands");
        PlayerPrefs.SetString("Feet", "feet");
        PlayerPrefs.SetString("Wings", "wings");
        PlayerPrefs.Save();
    }

    private void PopulateBosses() {

    }

}
