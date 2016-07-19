using UnityEngine;

public class BattleController : MonoBehaviour {

    private bool turnStarted;

    private PlayerStatistic boss;
    private Cache cache;
    private PlayerStatistic player;
    

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        this.turnStarted = false;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.player = this.cache.player
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
