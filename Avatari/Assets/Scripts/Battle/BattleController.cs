using UnityEngine;

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

    public void Attack() {
        if(!turnStarted) {
            turnStarted = true;
            Debug.Log("Attack");
            turnStarted = false;
        }
    }

    public void Flee() {
        if (!turnStarted) {
            turnStarted = true;
            Debug.Log("Flee");
            turnStarted = false;
        }
    }
}
