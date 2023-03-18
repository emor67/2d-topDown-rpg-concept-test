using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public enum BattleState { BEFORE, BATTLE, PLAYERTURN, ENEMYTURN, WIN, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    Collider2D collider1;
    Collider2D collider2;
    public GameObject _player;
    public GameObject _enemy;

    public GameObject player;
    public GameObject enemy;

    public Transform playerStation;
    public Transform enemyStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Canvas battleCanvas;

    public Text dialogText;

    private unit playerUnit;
    private unit enemyUnit;

    private bool hasCollided = false;


    void Start()
    {
        state = BattleState.BEFORE;
        collider1 = _player.GetComponent<Collider2D>();
        collider2 = _enemy.GetComponent<Collider2D>();
    }

    private void Update()
    {
        BattleCheck();
    }

    

void BattleCheck()
    {
        if (state == BattleState.BEFORE)
        {
            battleCanvas.gameObject.SetActive(false);
        }
        if (state == BattleState.BATTLE)
        {
            battleCanvas.gameObject.SetActive(true);
            StartCoroutine(SetupBattle());
        }
        isTouching();
    }

    private void isTouching()
    {
        
        if (!hasCollided)
        {
            if (collider1.IsTouching(collider2))
            {
                //Debug.Log("GameObject1 and GameObject2 are touching");
                state = BattleState.BATTLE;
                hasCollided = true;
            }
        }
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, playerStation);
        playerUnit = playerGO.GetComponent<unit>();
        
        GameObject enemyGO = Instantiate(enemy, enemyStation);
        enemyUnit = enemyGO.GetComponent<unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        dialogText.text = "OMG! Is this " + enemyUnit.unitName + "?";

        state = BattleState.PLAYERTURN;
        
        yield return new WaitForSeconds(2f);

        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isdead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        enemyHUD.SetHUD(enemyUnit);
        dialogText.text = "Attack's legit, dude";

        if (isdead)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " is comin' tru ya!!!";

        yield return new WaitForSeconds(1f);

        bool isdead = playerUnit.TakeDamage(enemyUnit.damage);
        
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHUD(playerUnit);
        
        yield return new WaitForSeconds(1f);

        if (isdead)
        {
            state = BattleState.LOST;
            EndBattle();
            
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WIN)
        {
            dialogText.text = "Hit da b*tch, man!";
            
        }
        else if(state == BattleState.LOST)
        {
            dialogText.text = "Nah man, you f*cked up!?";
            
        }
        StartCoroutine(CloseBattlePanel());
    }

    IEnumerator CloseBattlePanel()
    {
        yield return new WaitForSeconds(2f);

        battleCanvas.gameObject.SetActive(false);
    }
    void PlayerTurn()
    {
        dialogText.text = "Action time!!!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetHUD(playerUnit);

        dialogText.text = "Ohh, it feels good!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
}
