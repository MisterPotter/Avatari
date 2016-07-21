using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BossDialog : MonoBehaviour, IPointerDownHandler {

    public bool canChallenge { get; set; }
    public Boss boss { get; set; }

    private Cache cache;
    private Transform dialogSpawner;

    private const string levelInsufficient = "Your character's level isn't high enough to fight this boss.";

    private void Awake() {
        cache = Utility.LoadObject<Cache>("Cache");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!canChallenge) return;
        GameObject dialogPrefab = Resources.Load<GameObject>(
            "Prefabs/UI/Bosses/BossBattleDialog");
        if (dialogPrefab == null) {
            throw new System.Exception("Unequip dialog prefab was not found.");
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

        Image bossImage = dialog.transform.GetChild(3).GetComponent<Image>();
        Text bossName = dialog.transform.GetChild(4).GetComponent<Text>();
        Text bossLevel = dialog.transform.GetChild(5).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(6).GetComponent<Button>();
        Button decline = dialog.transform.GetChild(7).GetComponent<Button>();

        bossImage.sprite = cache.LoadBossSprite(boss.getSpriteName());
        bossName.text = boss.getName();
        bossLevel.text = "Level " + boss.getLevel();

        accept.onClick.AddListener(
            delegate {
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

}
