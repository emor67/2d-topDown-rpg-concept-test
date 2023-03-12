using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameAndHP;
    public int hp;

    public void SetHP(int hp)
    {
        this.hp = hp;
    }
    public void SetHUD(unit unit)
    {
        unit.currentHP = hp;
        nameAndHP.text = unit.unitName + "   Hp " + unit.currentHP + "/" + unit.maxHP;
    }
    
}
