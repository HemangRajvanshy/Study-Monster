using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyCombatant : StudyCombatant {

    public Problem problem;
    public int DamageUpperLim = 20;
    public int DamageLowerLim = 10;

    public int GetDamage()
    {
        int Damage = UnityEngine.Random.Range(DamageLowerLim, DamageUpperLim);
        return Damage;
    }

}

[Serializable]
public struct Problem
{
    public string ProblemText;
    public List<OptionSet> Parts;
}

[Serializable]
public struct OptionSet
{
    public string Correct;
    public string Option1;
    public string Option2;
    public string Option3;
}

