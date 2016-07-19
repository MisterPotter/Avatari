using UnityEngine;
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
        //TODO, calculate flee chance based on ratios then flee.
        //Show dialog for things that have changed.
        SceneManager.LoadScene("home");
    }
}
