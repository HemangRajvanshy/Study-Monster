﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ProblemManager : MonoBehaviour {

    public BattleManager BattleManager;

    public Text ProblemText;
    public Button Option1;
    public Button Option2;
    public Button Option3;
    public Button Option4;

    private Text Option1Text;
    private Text Option2Text;
    private Text Option3Text;
    private Text Option4Text;

    private EnemyCombatant EnemyCombatant;
    private Problem Problem;

    private UnityAction CorrectAnswer;
    private UnityAction WrongAnswer;

    void Start()
    {
        Option1Text = Option1.gameObject.GetComponentInChildren<Text>();
        Option2Text = Option2.gameObject.GetComponentInChildren<Text>();
        Option3Text = Option3.gameObject.GetComponentInChildren<Text>();
        Option4Text = Option4.gameObject.GetComponentInChildren<Text>();

        CorrectAnswer = new UnityAction(Correct);
        WrongAnswer = new UnityAction(Wrong);
    }

    public void Init(EnemyCombatant Enemy)
    {
        EnemyCombatant = Enemy;
        Problem = Enemy.problem;
        ProblemText.text = Enemy.problem.ProblemText;
        SetupOptions(0);
    }

    private void SetupOptions(int Part)
    {
        int correctOp = Random.Range(1, 5);
        switch(correctOp)
        {
            case 1:
                Option1Text.text = Problem.Parts[Part].Correct;
                Option2Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;
                break;
            case 2:
                Option2Text.text = Problem.Parts[Part].Correct;
                Option1Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;
                break;
            case 3:
                Option3Text.text = Problem.Parts[Part].Correct;
                Option2Text.text = Problem.Parts[Part].Option1;
                Option1Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;
                break;
            case 4:
                Option4Text.text = Problem.Parts[Part].Correct;
                Option2Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option1Text.text = Problem.Parts[Part].Option3;
                break;
        }
    }

    void Correct()
    {
        Debug.Log("TODO: Correct Answer");
    }

    void Wrong()
    {
        Debug.Log("TODO: Wrong Answer");
    }
}

