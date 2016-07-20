using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour {

    private bool turnStarted;

    private Cache cache;
    private PlayerStatistic boss;
    private PlayerStatistic player;
    

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        this.turnStarted = false;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.boss = Utility.DeepClone<PlayerStatistic>(this.cache.boss.getStats());
        this.player = Utility.DeepClone<PlayerStatistic>(this.cache.player.stats);
    }

    public void AttackHandler() {
        if(!turnStarted) {
            turnStarted = true;
            Debug.Log("Attack");
            turnStarted = false;
        }
    }

    public void FleeHandler() {
        if (!turnStarted) {
            turnStarted = true;
            Flee();
            turnStarted = false;
        }
    }

    private void Flee() {
        System.Random rand = new System.Random();
        double chance = Chance(this.player, this.boss);
        bool fleeSuccess = rand.Next(100) < chance ? true : false;

        //Show dialog for things that have changed.
        SceneManager.LoadScene("home");
    }

    public double Chance(PlayerStatistic attacker, PlayerStatistic victim) {
        if (attacker.agility.CurrentValueDouble < victim.agility.CurrentValueDouble) {
            return (attacker.agility.CurrentValueDouble / victim.agility.CurrentValueDouble * 100);
        } else {
            return 95;
        }
    }

    //Strength = damage value, defense = % damage reduction
    public double calcDamage(PlayerStatistic attacker, PlayerStatistic victim) {
        return (attacker.strength.CurrentValueDouble - (victim.defense.CurrentValueDouble / 100));
    }
}
