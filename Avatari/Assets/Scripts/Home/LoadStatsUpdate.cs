using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Globalization;

/**
 * @author: Charlotte
 * @summary: Updates the player's statistics based on their Fitbit data since last login.
 * Also generates UI for the updated stats popup
 */
public class LoadStatsUpdate : MonoBehaviour {

    private static LoadStatsUpdate disIsYou;

    private PlayerStatistic initStats;
    private PlayerStatistic updatedStats;

    private bool isFirstLogin;
    private DateTime lastLoginDate;

    private Cache cache;
    private Transform dialogSpawner;

    private const string prefabPath = "Prefabs/UI/StatisticsPopup/StatsDialog";
    private const string lastLogin = "lastLogin";
    private const string dateFormat = "yyyy-MM-dd";

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

        UpdateStats();

        CleanUpExistingDialogs();

        if (NeedsUpdate(lastLoginDate)) {
            CreateStatsDialog();
        } else {
            CreateWelcomeDialog();
        }
    }

    private void UpdateStats() {
        
        initStats = GetPlayerStats();
               
        lastLoginDate = GetLastLogin();

        // If this is the user's first login, or if they've logged in already today,
        // then we don't need to update the stats
        updatedStats = null;
        if (NeedsUpdate(lastLoginDate)) {
            UpdatePlayerStats();
            updatedStats = GetPlayerStats();
        }

        SetTodayAsLastLogin();
    }

    /* Returns default DateTime object if this is the first login*/
    private DateTime GetLastLogin() {
        if (PlayerPrefs.HasKey(lastLogin)) {
            string lastLoginStr = PlayerPrefs.GetString(lastLogin);
            isFirstLogin = false;
            return DateTime.ParseExact(lastLoginStr, dateFormat, CultureInfo.InvariantCulture);
        } else {
            isFirstLogin = true;
            return new DateTime();
        }
    }
    
    private void SetTodayAsLastLogin() {
        PlayerPrefs.SetString(lastLogin, DateTime.Today.ToString(dateFormat));
    }

    /* Returns true if we need to update the stats */
    private bool NeedsUpdate(DateTime date) {
        return (!isFirstLogin && date < DateTime.Today);
    }

    private void UpdatePlayerStats() {
        this.cache.player.stats.UpdatePlayerStatisticsSince(cache, lastLoginDate);
    }

    private PlayerStatistic GetPlayerStats() {
        return Utility.DeepClone<PlayerStatistic>(this.cache.player.stats);
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

        int addedStrength = updatedStats.strength.CurrentValue - initStats.strength.CurrentValue;
        int addedDefense = updatedStats.defense.CurrentValue - initStats.defense.CurrentValue;
        int addedAgility = updatedStats.agility.CurrentValue - initStats.agility.CurrentValue;
        int addedExp = updatedStats.experience.CurrentValue - initStats.experience.CurrentValue;

        strengthValue.text = (addedStrength > 0) ? "+ " + addedStrength.ToString() : addedStrength.ToString();
        defenseValue.text = (addedDefense > 0) ? "+ " + addedDefense.ToString() : addedDefense.ToString();
        agilityValue.text = (addedAgility > 0) ? "+ " + addedAgility.ToString() : addedAgility.ToString();
        expValue.text = (addedExp > 0) ? "+" + addedExp.ToString() + " xp" : addedExp.ToString() + " xp";
    }

    private void CreateWelcomeDialog() {
        // TODO: implement a welcome/help screen as per issue #63
    }
}
