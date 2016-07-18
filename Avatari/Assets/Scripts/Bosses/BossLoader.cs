using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/**
* @author: Denholm
* @summary: Handler responsible for populating boss scene
* */
public class BossLoader : MonoBehaviour {

    private Cache cache;
    private RectTransform content;
    private GameObject panelPrefab;
    private Transform panelSpawner;
    private Transform dialogSpawner;
    private const float rowVertOffset = 60.0f;

    private int playerLevel;
    private List<Boss> bosses;

    private void Awake () {
        Initialize();
        GetPlayerLevel();
        LoadBosses();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        content = Utility.LoadObject<RectTransform>("BossContent");
        panelPrefab = Resources.Load<GameObject>("Prefabs/UI/Bosses/BossPanel");
        panelSpawner = Utility.LoadObject<Transform>("BossPanelSpawner");
        dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");
    }

    private void LoadBosses() {
        bosses = this.cache.LoadBosses();
        content.sizeDelta = new Vector2(0.0f, bosses.Count*rowVertOffset);
        Vector3 offset = Vector3.down * rowVertOffset;

        int i = 0;
        foreach (var boss in bosses) {
            GameObject clone = (GameObject)Instantiate(panelPrefab, 
                (offset*i++), Quaternion.identity);
            clone.transform.SetParent(panelSpawner, false);

            // boss is challengeable if the player has that level or greater
            bool canChallenge = playerLevel >= boss.getLevel();

            Text bossTitle = clone.transform.GetChild(0).GetComponent<Text>();
            if (canChallenge) {
                bossTitle.text = boss.getName();
            } else {
                bossTitle.text = "???";
            }
            Text bossRequirement = clone.transform.GetChild(1).GetComponent<Text>();
            bossRequirement.text = boss.getRequirement();

            Image bossIcon = clone.transform.GetChild(2).GetComponent<Image>();
            if (canChallenge) {
                bossIcon.sprite = cache.LoadBossSprite(boss.getSpriteName());
            } else {
                bossIcon.sprite = cache.LoadBossSprite("mystery");
            }

            Image bossInfo = clone.transform.GetChild(3).GetComponent<Image>();
            bossInfo.sprite = cache.LoadUtilitySprite("info");

            BossDialog bossScript = clone.transform.GetComponent<BossDialog>();
            bossScript.boss = boss;
            if (canChallenge) {
                bossScript.canChallenge = true;
            }
        }
        
    }

    private void GetPlayerLevel() {
        playerLevel = this.cache.LoadPlayerLevel();
    }
}
