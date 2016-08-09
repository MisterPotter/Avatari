using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 *  It's all a hack...
 */
public class BattleController : MonoBehaviour {

    /*
     *  Game related elements.
     */
    private bool turnStarted;
    private Cache cache;
    private PlayerStatistic boss;
    private PlayerStatistic player;

    /*
     *  Timer to show hp drop to zero... sigh... hacky
     */
    private bool gameOver;
    private float gameOverTimer;
    private const float gameOverTimeout = 2.0f;
    private bool gameOverDialog;


    /*
     *  Spawners
     */
    private Transform dialogSpawner;

    /*
     *  UI elements, redudant from loading scripts.
     */
    private Image playerImage;
    private Text playerHealth;
    private Text bossHealth;

    /*
     *  Constants
     */
    private const int DamageMultiplier = 4;

    /*
     *  UI String constants
     */
    private const string HealthFormat = "Health: {0}";
    private const string FleeSuccess = "You got away!";
    private const string FleeFailed = "Failed to get away...";
    private const string PlayerAttackTitle = "You Attack...";
    private const string PlayerHit = "{0} was hit! {1} Damage dealt!";
    private const string PlayerMissed = "{0} managed to evade the attack!";
    private const string BossAttackTitle = "{0} Attacks...";
    private const string BossHit = "You were hit! -{0} HP";
    private const string BossMissed = "You managed to evade the attack!";
    private const string GameOverSuccessTitle = "Victory!";
    private const string GameOverSuccess = "You've defeated {0}!";
    private const string GameOverDiedTitle = "Game over";
    private const string GameOverDied = "{0} has defeated you";

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        this.turnStarted = false;
        this.cache = Utility.LoadObject<Cache>("Cache");
        this.boss = Utility.DeepClone<PlayerStatistic>(this.cache.boss.getStats());
        this.player = this.cache.player.stats;
        this.dialogSpawner = Utility.LoadObject<Transform>("DialogSpawner");

        Transform statsPanel = Utility.LoadObject<Transform>("StatsPanel");
        this.playerHealth = statsPanel.GetChild(0)
            .GetChild(0)
            .GetComponent<Text>();

        this.bossHealth = statsPanel.GetChild(1)
            .GetChild(0)
            .GetComponent<Text>();

        Transform fightPanel = Utility.LoadObject<Transform>("FightPanel");
        this.playerImage = fightPanel.GetChild(0).GetComponent<Image>();

        this.gameOver = false;
        this.gameOverTimer = 0.0f;
        this.gameOverDialog = false;
    }

    private void Update() {
        if(this.gameOver) {
            this.gameOverTimer += Time.deltaTime;
            if(this.gameOverTimer > gameOverTimeout && !this.gameOverDialog) {
                bool win = this.player.health.CurrentValue > 0;
                CreateGameOverDialog(win);
            }
        }
    }

    public void AttackHandler() {
        if(!turnStarted) {
            turnStarted = true;
            CreateAttackDialog(true);
        }
    }

    private void CreateAttackDialog(bool success) {
        if (success) {
            int newHealth = this.boss.health.CurrentValue - PlayerDamage();
            this.boss.health.CurrentValue = Math.Max(0, newHealth);
            this.bossHealth.text = String.Format(
                HealthFormat, this.boss.health.CurrentValue
            );
        }
        CreateOutcomeDialog(true, success);
    }

    private int PlayerDamage() {
        return (int)CalcDamage(this.player, this.boss);
    }

    public void FleeHandler() {
        if (!turnStarted) {
            turnStarted = true;
            CreateFleeDialog(Flee());
        }
    }

    private void CreateFleeDialog(bool success) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Battles/Dialogs/FleeDialog");
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();

        description.text = success ? FleeSuccess : FleeFailed;
        accept.onClick.AddListener(
            delegate {
                if (success) {
                    PushItemsReturnHome();
                } else {
                    BossAttack();
                }
                Destroy(dialog);
            }
        );

        dialog.transform.SetParent(this.dialogSpawner, false);
    }

    private int BossDamage() {
        return (int)CalcDamage(this.boss, this.player);
    }

    private void BossAttack() {
        if (Hit(this.boss, this.player)) {
            int newHealth = this.player.health.CurrentValue - BossDamage();
            this.player.health.CurrentValue = Mathf.Max(0, newHealth);
            this.playerHealth.text = String.Format(HealthFormat, this.player.health.CurrentValue);
            // create dialog
            CreateOutcomeDialog(false, true);
        } else {
            CreateOutcomeDialog(false, false);
        }
    }

    private void CreateOutcomeDialog(bool playerAttack, bool hit) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Battles/Dialogs/OutcomeDialog");
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );

        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();

        if (!playerAttack) {
            title.text = String.Format(BossAttackTitle, this.cache.boss.getName());
            description.text = hit ? String.Format(BossHit, BossDamage())
                : BossMissed;
        } else {
            title.text = PlayerAttackTitle;
            description.text = hit ? 
                String.Format(
                    PlayerHit, this.cache.boss.getName(), PlayerDamage()
                ) : String.Format(PlayerMissed, this.cache.boss.getName());
        }
        accept.onClick.AddListener(
            delegate {
                Destroy(dialog);
                if (!playerAttack && hit) {
                    HandleGameOver();
                } else if (playerAttack && hit) {
                    HandleGameOver();
                    if(!this.gameOver) {
                        BossAttack();
                    }
                } else {
                    this.turnStarted = false;
                }
            }
        );
        dialog.transform.SetParent(this.dialogSpawner, false);
    }

    private void HandleGameOver() {
        // Handle death or new turn
        if (player.health.CurrentValue == 0) {
            Sprite[] playerSpriteSheet = Resources.LoadAll<Sprite>("Characters/" +
                this.cache.player.sprite);
            this.playerImage.sprite = Utility.GetSprite("defeated", playerSpriteSheet);
            this.gameOver = true;
        } else if (boss.health.CurrentValue == 0) {
            this.gameOver = true;
        } else {
            this.turnStarted = false;
        }
    }

    private void CreateGameOverDialog(bool win) {
        this.gameOverDialog = true;
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Battles/Dialogs/GameOverDialog");
        GameObject dialog = (GameObject)Instantiate(
            prefab, new Vector2(0.0f, 0.0f), Quaternion.identity
        );

        Text title = dialog.transform.GetChild(0).GetComponent<Text>();
        Text description = dialog.transform.GetChild(1).GetComponent<Text>();
        Button accept = dialog.transform.GetChild(2).GetComponent<Button>();

        title.text = win ? GameOverSuccessTitle : GameOverDiedTitle;
        description.text = win ?
            String.Format(GameOverSuccess, this.cache.boss.getName()) :
            String.Format(GameOverDied, this.cache.boss.getName());

        /* Store battle information for reward dialog */
        LastBattle.boss = this.cache.boss;
        LastBattle.rewardCollected = false;
        if (win) {
            LastBattle.battleWon = true;
        } else {
            LastBattle.battleWon = false;
        }

        accept.onClick.AddListener(
            delegate {
                Destroy(dialog);
                PushItemsReturnHome();
            }
        );

        dialog.transform.SetParent(this.dialogSpawner, false);
    }

    private void PushItemsReturnHome() {
        Requests requests = new Requests(this.cache);
        StartCoroutine(requests.PushEquippedItems());
        StartCoroutine(requests.PushTari());
        StartCoroutine(LoadHome(requests));

    }

    private IEnumerator LoadHome(Requests requests) {
        while(requests.RequestNotFinished()) {
            yield return null;
        }
        SceneManager.LoadScene("home");
    }

    private bool Flee() {
        System.Random rand = new System.Random();
        double chance = Chance(this.player, this.boss);
        return rand.Next(100) < chance ? true : false;
    }

    private bool Hit(PlayerStatistic attacker, PlayerStatistic victim) {
        System.Random rand = new System.Random();
        double chance = Chance(attacker, victim);
        return rand.Next(100) < chance ? true : false;
    }

    public double Chance(PlayerStatistic attacker, PlayerStatistic victim) {
        if (attacker.agility.CurrentValueDouble < victim.agility.CurrentValueDouble) {
            return (attacker.agility.CurrentValueDouble / victim.agility.CurrentValueDouble * 100);
        } else {
            return 95;
        }
    }

    //Strength = damage value, defense = % damage reduction
    public double CalcDamage(PlayerStatistic attacker, PlayerStatistic victim) {
        return (attacker.strength.CurrentValueDouble - (victim.defense.CurrentValueDouble / 100)) * DamageMultiplier;
    }
}
