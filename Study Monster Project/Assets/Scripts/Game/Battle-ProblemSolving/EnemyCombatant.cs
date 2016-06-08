using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(NPCController))]
public class EnemyCombatant : StudyCombatant {

    public Problem problem;
    public List<string> AfterLooseDialogue = new List<string>();
    public int DamageUpperLim = 20;
    public int DamageLowerLim = 10;
    public int CombatantNumber;

    private NPCController Npc;

    new void Start()
    {
        base.Start();
        Npc = GetComponent<NPCController>();
        if(Npc.Combatant)
        {
            CheckIfFought();
        }
    }

    private void CheckIfFought()
    {
        if(Main.Instance.player.GameData.NPCFought.Contains(CombatantNumber))
        {
            Lost();
        }
        //else we have not fought
    }

    public int GetDamage()
    {
        int Damage = UnityEngine.Random.Range(DamageLowerLim, DamageUpperLim);
        return Damage;
    }

    public void Lost()
    {
        Npc.Dialogue = AfterLooseDialogue;
        Npc.FoughtWith();
        Main.Instance.player.NPCFought(CombatantNumber);
    }

    //public void Won()
    //{
    //    Npc.Dialogue = AfterWinDialogue;
    //    Npc.FoughtWith();
    //}
}

[Serializable]
public struct Problem
{
    public string ProblemText;
    public Sprite ProblemImage;
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

