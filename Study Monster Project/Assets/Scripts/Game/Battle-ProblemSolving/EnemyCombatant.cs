using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyCombatant : StudyCombatant {

    public Problem problem;

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

