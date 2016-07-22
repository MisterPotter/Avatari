using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossRewardLoader : MonoBehaviour {

    public Boss boss;

    private Cache cache;
    private Transform dialogSpawner;

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
        if (LastBattle.battleWon && !LastBattle.rewardCollected) {
            boss = LastBattle.boss;
            GenerateRewardDialog();
        }
    }

    public void GenerateRewardDialog() {

        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Goals/CompletionDialog");
        if (dialogPrefab == null) {
            throw new System.Exception("Goal completion dialog prefab was not found.");
        }
        CleanUpExistingDialogs();
        CreateDialog(dialogPrefab);
    }

    private void CleanUpExistingDialogs() {
        foreach (Transform dialog in this.dialogSpawner) {
            Destroy(dialog.gameObject);
        }
    }

    private void CreateDialog(GameObject prefab) {
        GameObject dialog = (GameObject)Instantiate(prefab, new Vector2(0.0f, 0.0f), Quaternion.identity);

        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text bossDescription = dialog.transform.GetChild(1).GetComponent<Text>();
        Text bossLevel = dialog.transform.GetChild(2).GetComponent<Text>();
        Text experienceValue = dialog.transform.GetChild(3).GetComponent<Text>();
        Text statValue = dialog.transform.GetChild(4).GetComponent<Text>();
        Button confirm = dialog.transform.GetChild(5).GetComponent<Button>();

        title.text = "BOSS DEFEATED";
        bossDescription.text = this.boss.getName();
        bossLevel.text = "Level " + this.boss.getLevel();

        // Generate random reward for defeating boss
        System.Random randy = new System.Random();
        int statBoost = randy.Next(5, 12);
        string stat = "+" + statBoost + " ";
        switch (GetRandomStat(randy)) {
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
        int expBoost = randy.Next(200, 500);
        experienceValue.text = "+" + expBoost + " xp";
        this.cache.player.stats.experience.CurrentValue += expBoost;
        this.cache.player.stats.UpdateLevel();
        confirm.onClick.AddListener(
            delegate {
                LastBattle.rewardCollected = true;
                Destroy(dialog);
            }
        );

        dialog.transform.SetParent(this.dialogSpawner, false);
    }


    private Statistic.Type GetRandomStat(System.Random randy) {
        // this function is making me randy
        return (Statistic.Type)randy.Next(3, 6);
    }
}
