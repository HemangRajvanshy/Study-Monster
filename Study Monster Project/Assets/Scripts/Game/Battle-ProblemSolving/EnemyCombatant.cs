using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyCombatant : StudyCombatant {

    public Problem problem;
    public List<string> AfterWinDialogue = new List<string>();
    public List<string> AfterLooseDialogue = new List<string>();
    public int DamageUpperLim = 20;
    public int DamageLowerLim = 10;

    public int GetDamage()
    {
        int Damage = UnityEngine.Random.Range(DamageLowerLim, DamageUpperLim);
        return Damage;
    }

    public void Lost()
    {
        GetComponent<NPCController>().Dialogue = AfterLooseDialogue;
        GetComponent<NPCController>().FoughtWith();
    }

    public void Won()
    {
        GetComponent<NPCController>().Dialogue = AfterWinDialogue;
        GetComponent<NPCController>().FoughtWith();
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

