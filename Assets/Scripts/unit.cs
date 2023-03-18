using UnityEngine;

public class unit : MonoBehaviour
{
    public string unitName;
    
    public int maxHP;
    public int currentHP;

    public int damage;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    
}
