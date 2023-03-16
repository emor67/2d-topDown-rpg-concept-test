using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameAndHP;
    private int _hp = 60;

    public void SetHP(int hp)
    {
        _hp = hp;
    }
    public void SetHUD(unit unit)
    {
        nameAndHP.text = unit.unitName + "   Hp " + _hp + "/" + unit.maxHP;
    }
    
}
