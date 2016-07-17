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

        confirm.onClick.AddListener(
            delegate {
                Destroy(dialog);
            });

        dialog.transform.SetParent(dialogSpawner, false);
    }
	
}
