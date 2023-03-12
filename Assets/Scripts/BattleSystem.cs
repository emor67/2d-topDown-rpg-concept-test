using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum BattleState { BEFORE, PLAYERTURN, ENEMYTURN, WIN, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public GameObject enemy;

    public Transform playerStation;
    public Transform enemyStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogText;

    private unit playerUnit;
    private unit enemyUnit;

    void Start()
    {
        state = BattleState.BEFORE;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, playerStation);
        playerUnit = playerGO.GetComponent<unit>();
        
        GameObject enemyGO = Instantiate(enemy, enemyStation);
        enemyUnit = enemyGO.GetComponent<unit>();

        dialogText.text = "OMG! Is this " + enemyUnit.unitName + "?";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isdead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
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

        yield return new WaitForSeconds(3f);
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " is comin' tru ya!!!";

        yield return new WaitForSeconds(2f);

        bool isdead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

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
        }else if(state == BattleState.LOST)
        {
            dialogText.text = "Nah man, you f*cked up!?";
        }
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
}
