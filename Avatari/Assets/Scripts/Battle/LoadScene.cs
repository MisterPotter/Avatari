using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadScene : MonoBehaviour {

    private Cache cache;
    private Transform fightPanel;
    private Transform statsPanel;

    private void Awake() {
        Initialize();
        LoadFightArea();
    }

    private void Initialize() {
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.fightPanel = Utility.LoadObject<Transform>("FightPanel");
        this.statsPanel = Utility.LoadObject<Transform>("StatsPanel");
    }

    private void LoadFightArea() {
        Image userSprite = fightPanel.GetChild(0).GetComponent<Image>();
        Image bossSprite = fightPanel.GetChild(1).GetComponent<Image>();
        Image areaSprite = fightPanel.GetComponent<Image>();

        // Load player sprite
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Characters/" + this.cache.player.sprite);
        if (spriteSheet.Length == 0) {
            throw new Exception("Character sprite: " + this.cache.player.sprite +
                " could not be found.");
        }
        userSprite.sprite = Utility.GetSprite("idle", spriteSheet);

        // Load boss sprite
        bossSprite.sprite = Resources.Load<Sprite>("Characters/PrototypeBosses/"
            + this.cache.boss.getSpriteName());

        // Load Area
        areaSprite.sprite = Resources.Load<Sprite>("Sprites/Areas/"
            + this.cache.player.area.spriteName);
    }
}
