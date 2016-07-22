using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Globalization;
using UnityEngine.UI;

public class CompletionDialogLoader : MonoBehaviour, IPointerDownHandler {

    public Goal goal;

    private Cache cache;
    private Transform dialogSpawner;

    private const string dateFormat = "yyyy-MM-dd";

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!CanBeClaimed()) return;

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Goals/CompletionDialog");
        if (dialogPrefab == null) {
            throw new System.Exception("Goal completion dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateDialog(dialogPrefab);
        SetLastClaimedDate();
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    private void CreateDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(prefab, new Vector2(0.0f, 0.0f), Quaternion.identity);

        Text goalDescription = dialog.transform.GetChild(1).GetComponent<Text>();
        Text goalProgress = dialog.transform.GetChild(2).GetComponent<Text>();
        Text experienceValue = dialog.transform.GetChild(3).GetComponent<Text>();
        Text statValue = dialog.transform.GetChild(4).GetComponent<Text>();
        Button confirm = dialog.transform.GetChild(5).GetComponent<Button>();

        goalDescription.text = goal.description;
        goalProgress.text = goal.progress + "/" + goal.goal;

        // Generate random reward for achieving goal
        System.Random randy = new System.Random();
        int statBoost = randy.Next(3, 10);
        string stat = "+" + statBoost + " ";
        switch ( GetRandomStat(randy)) {
            case Statistic.Type.Agility:
                stat += "Agility";
                this.cache.player.stats.agility.CurrentValue += statBoost;
                break;
            case Statistic.Type.Defense:
                stat += "Defense";
                this.cache.player.stats.defense.CurrentValue += statBoost;
                break;
            case Statistic.Type.Strength:
            default:
                stat += "Strength";
                this.cache.player.stats.strength.CurrentValue += statBoost;
                break;
        }

        statValue.text = stat;

        // Generate random experience boost
        int expBoost = randy.Next(100, 401);
        experienceValue.text = "+" + expBoost + " xp";
        this.cache.player.stats.experience.CurrentValue += expBoost;
        this.cache.player.stats.UpdateLevel();

        confirm.onClick.AddListener(
            delegate {
                Destroy(dialog);
            }
        );
        
        dialog.transform.SetParent(this.dialogSpawner, false);
    }

    private bool CanBeClaimed() {
        if (PlayerPrefs.HasKey(goal.description)) {
            DateTime lastClaimed = DateTime.ParseExact( PlayerPrefs.GetString(goal.description), 
                dateFormat, CultureInfo.InvariantCulture );
            if ( lastClaimed == DateTime.Today) {
                return false;
            }
        }
        return true;
    }

    private void SetLastClaimedDate() {
        PlayerPrefs.SetString(goal.description, DateTime.Today.ToString(dateFormat));
    }

    private Statistic.Type GetRandomStat(System.Random randy) {
        // this function is making me randy
        return (Statistic.Type) randy.Next(3, 6);
    }
}
