using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BossLoader : MonoBehaviour {

    private Cache cache;
    private RectTransform content;
    private GameObject panelPrefab;
    private Transform panelSpawner;
    private const float rowVertOffset = 60.0f;

    private void Awake () {
        Initialize();
        LoadBosses();
    }

    private void Initialize() {
        cache = Utility.LoadObject<Cache>("Cache");
        content = Utility.LoadObject<RectTransform>("BossContent");
        panelPrefab = Resources.Load<GameObject>("Prefabs/UI/Bosses/BossPanel");
        panelSpawner = Utility.LoadObject<Transform>("BossPanelSpawner");
    }

    private void LoadBosses() {
        List<Boss> bosses = this.cache.LoadBosses();
        content.sizeDelta = new Vector2(0.0f, bosses.Count*rowVertOffset);
        Vector3 offset = Vector3.down * rowVertOffset;

        int i = 0;
        foreach (var boss in bosses) {
            GameObject clone = (GameObject)Instantiate(panelPrefab, 
                (offset*i++), Quaternion.identity);
            clone.transform.SetParent(panelSpawner, false);

            Text bossTitle = clone.transform.GetChild(0).GetComponent<Text>();
            bossTitle.text = boss.getName();

            Text bossRequirement = clone.transform.GetChild(1).GetComponent<Text>();
            bossRequirement.text = boss.getRequirement();

            Image bossIcon = clone.transform.GetChild(2).GetComponent<Image>();
            bossIcon.sprite = cache.LoadBossSprite(boss.getSpriteName());

            Image bossInfo = clone.transform.GetChild(3).GetComponent<Image>();
            bossInfo.sprite = cache.LoadUtilitySprite("info");
        }
        
    }
}
