using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StudyCombatant : MonoBehaviour {

    public int TotalHealth = 100;

    protected int Health;

    protected virtual void Start()
    {
        Health = TotalHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void HealDamage(int hp)
    {
        Health += hp;
    }

    public int GetHealth()
    {
        return Health; 
    }
}
