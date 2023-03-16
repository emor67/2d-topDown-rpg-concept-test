using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameAndHP;
    private int _hp;

    public void SetHP(int hp)
    {
        _hp = hp;
    }
    public void SetHUD(unit unit)
    {
        unit.currentHP = _hp;
        nameAndHP.text = unit.unitName + "   Hp " + unit.currentHP + "/" + unit.maxHP;
    }
    
}
