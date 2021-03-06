﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadScene : MonoBehaviour {

    private Cache cache;
    private Transform fightPanel;
    private Transform statsPanel;
    private BattleController battleController;

    private const string healthFormat = "HP: {0}";
    private const string hitChanceFormat = "Hit: {0:0.#}%";
    private const string damageFormat = "Dmg: {0}";

    private void Awake() {
        Initialize();
        LoadFightArea();
        LoadStatsArea();
    }

    private void Initialize() {
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.fightPanel = Utility.LoadObject<Transform>("FightPanel");
        this.statsPanel = Utility.LoadObject<Transform>("StatsPanel");
        this.battleController = Utility.LoadObject<BattleController>("BattleController");
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

    private void LoadStatsArea() {
        Transform userStats = this.statsPanel.GetChild(0);
        Transform bossStats = this.statsPanel.GetChild(1);

        PopulateStatsPanel(userStats, this.cache.player.stats,
            this.cache.boss.getStats());
        PopulateStatsPanel(bossStats, this.cache.boss.getStats(),
            this.cache.player.stats);
    }

    private void PopulateStatsPanel(Transform panel, PlayerStatistic me, PlayerStatistic against) {
        Text health = panel.GetChild(0).GetComponent<Text>();
        Text hitChance = panel.GetChild(1).GetComponent<Text>();
        Text damage = panel.GetChild(2).GetComponent<Text>();

        // Please don't have zero stats
        double hitChanceDealt   = this.battleController.Chance(me, against);
        double damageDealt      = this.battleController.CalcDamage(me, against);

        health.text = String.Format(healthFormat, me.health.CurrentValue);
        hitChance.text = String.Format(hitChanceFormat, hitChanceDealt);
        damage.text = String.Format(damageFormat, (int)damageDealt);
    }
}
