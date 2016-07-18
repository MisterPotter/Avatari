using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadStatsUpdate : MonoBehaviour {

    private static LoadStatsUpdate disIsYou;

    private PlayerStatistic initStats;
    private PlayerStatistic updatedStats;

    private Cache cache;
    private Transform dialogSpawner;

    private const string prefabPath = "Prefabs/UI/StatisticsPopup/StatsDialog";

	void Start () {
        if (disIsYou == null) {
            Initialize();
        } else if (disIsYou != this) {
            Destroy(this.gameObject);
        }
	}

    private void Initialize() {
        DontDestroyOnLoad(gameObject);
        disIsYou = this;

        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");

        initStats = this.cache.player.stats;

        this.cache.player.stats.UpdatePlayerStatistics();
        updatedStats = this.cache.player.stats;
        CleanUpExistingDialogs();
        CreateStatsDialog();
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    private void CreateStatsDialog() {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null) {
            throw new System.Exception("Statistics dialog prefab not found.");
        }
        GameObject dialog = (GameObject)Instantiate(prefab, new Vector2(0.0f, 0.0f), Quaternion.identity);

        Button confirm = dialog.transform.Find("Confirm").GetComponent<Button>();

        IncrementStats(dialog);

        confirm.onClick.AddListener(
            delegate {
                Destroy(dialog);
            });

        dialog.transform.SetParent(dialogSpawner, false);
    }

    private void IncrementStats(GameObject dialog) {
        Text expValue = dialog.transform.Find("ExperienceValue").GetComponent<Text>();
        Text strengthValue = dialog.transform.Find("StrengthValue").GetComponent<Text>();
        Text defenseValue = dialog.transform.Find("DefenseValue").GetComponent<Text>();
        Text agilityValue = dialog.transform.Find("AgilityValue").GetComponent<Text>();

        // TODO: Not sure if this will actually work this way until can access player fitbit data
        int addedStrength = updatedStats.strength.CurrentValue - initStats.strength.CurrentValue;
        int addedDefense = updatedStats.defense.CurrentValue - initStats.defense.CurrentValue;
        int addedAgility = updatedStats.agility.CurrentValue - initStats.agility.CurrentValue;
        int addedExp = updatedStats.experience.CurrentValue - initStats.experience.CurrentValue;

        strengthValue.text = (addedStrength > 0) ? "+ " + addedStrength.ToString() : addedStrength.ToString();
        defenseValue.text = (addedDefense > 0) ? "+ " + addedDefense.ToString() : addedDefense.ToString();
        agilityValue.text = (addedAgility > 0) ? "+ " + addedAgility.ToString() : addedAgility.ToString();
        expValue.text = (addedExp > 0) ? "+" + addedExp.ToString() + " xp" : addedExp.ToString() + " xp";
    }
}

