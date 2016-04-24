using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ProblemManager : MonoBehaviour {

    public BattleManager BattleMngr;

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
    private int CurrentPart;

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
        CurrentPart = 0;
        SetupOptions(CurrentPart);
    }

    private void SetupOptions(int Part)
    {
        SetupOptions();
        int correctOp = Random.Range(1, 5);
        switch(correctOp)
        {
            case 1:
                Option1Text.text = GameManager.Instance.TextType.DecodeText(Problem.Parts[Part].Correct);
                Option2Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;

                Option1.onClick.AddListener(CorrectAnswer);
                Option2.onClick.AddListener(WrongAnswer);
                Option3.onClick.AddListener(WrongAnswer);
                Option4.onClick.AddListener(WrongAnswer);
                break;
            case 2:
                Option2Text.text = GameManager.Instance.TextType.DecodeText(Problem.Parts[Part].Correct);
                Option1Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;

                Option1.onClick.AddListener(WrongAnswer);
                Option2.onClick.AddListener(CorrectAnswer);
                Option3.onClick.AddListener(WrongAnswer);
                Option4.onClick.AddListener(WrongAnswer);
                break;
            case 3:
                Option3Text.text = GameManager.Instance.TextType.DecodeText(Problem.Parts[Part].Correct);
                Option2Text.text = Problem.Parts[Part].Option1;
                Option1Text.text = Problem.Parts[Part].Option2;
                Option4Text.text = Problem.Parts[Part].Option3;

                Option1.onClick.AddListener(WrongAnswer);
                Option2.onClick.AddListener(WrongAnswer);
                Option3.onClick.AddListener(CorrectAnswer);
                Option4.onClick.AddListener(WrongAnswer);
                break;
            case 4:
                Option4Text.text = GameManager.Instance.TextType.DecodeText(Problem.Parts[Part].Correct);
                Option2Text.text = Problem.Parts[Part].Option1;
                Option3Text.text = Problem.Parts[Part].Option2;
                Option1Text.text = Problem.Parts[Part].Option3;

                Option1.onClick.AddListener(WrongAnswer);
                Option2.onClick.AddListener(WrongAnswer);
                Option3.onClick.AddListener(WrongAnswer);
                Option4.onClick.AddListener(CorrectAnswer);
                break;
        }
    }

    private void SetupOptions()
    {
        Option1.interactable = true;
        Option1.onClick.RemoveAllListeners();
        Option2.interactable = true;
        Option2.onClick.RemoveAllListeners();
        Option3.interactable = true;
        Option3.onClick.RemoveAllListeners();
        Option4.interactable = true;
        Option4.onClick.RemoveAllListeners();
    }

    void Correct()
    {
        if(CurrentPart < Problem.Parts.Count-1)
        {
            CurrentPart++;
            BattleMngr.UpdateCombat(true);
            SetupOptions(CurrentPart);
        }
        else
        {
            BattleMngr.UpdateCombat(true);
        }
    }

    void Wrong()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false; // Set the selected button to uninteractable
        BattleMngr.UpdateCombat(false);
    }
}

