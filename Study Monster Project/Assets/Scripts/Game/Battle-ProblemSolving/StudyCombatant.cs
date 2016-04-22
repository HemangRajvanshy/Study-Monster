using UnityEngine;
using System.Collections;

public class StudyCombatant : MonoBehaviour {

    public int Health = 100;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void HealDamage(int hp)
    {
        Health += hp;
    }
}
