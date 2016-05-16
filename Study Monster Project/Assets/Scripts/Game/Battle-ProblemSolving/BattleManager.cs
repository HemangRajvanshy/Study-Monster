using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour {

    public Canvas BattleCanvas;
    public ProblemManager ProblemManager;    

    public PlayerCombatant Player;
    public Image PlayerHealthBar;
    private EnemyCombatant EnemyCombatant;
    public Image EnemyHealthBar;
       
    void Start()
    {
        BattleCanvas.enabled = false;
    }

    public void InitializeCombat(EnemyCombatant Combatant)
    {
        BattleCanvas.enabled = true;

        EnemyCombatant = Combatant;
        UpdateHealth(PlayerHealthBar, Player.GetHealth());
        UpdateHealth(EnemyHealthBar, EnemyCombatant.GetHealth());

        ProblemManager.Init(EnemyCombatant);
    }

    public void UpdateCombat(bool Correct)
    {
        if(Correct)
        {
            EnemyCombatant.TakeDamage(EnemyCombatant.TotalHealth/EnemyCombatant.problem.Parts.Count);
            UpdateHealth(EnemyHealthBar, EnemyCombatant.GetHealth());
            if (EnemyCombatant.GetHealth() == 0)
                EndCombat(true);
        }
        else
        {
            Player.TakeDamage(EnemyCombatant.GetDamage());
            UpdateHealth(PlayerHealthBar, Player.GetHealth());
            if (Player.GetHealth() == 0)
                EndCombat(false);
        }
    }

    public void EndCombat(bool win)
    {
        GameManager.Instance.EndCombat();
        if (win)
            EnemyCombatant.Lost();
        //else //Enemies don't do anything when they win, the game just loads from last save point.
        //    EnemyCombatant.Won();
        Player.GetComponent<PlayerController>().Talk(EnemyCombatant.GetComponent<IInteractable>());
        BattleCanvas.enabled = false;
    }

    private void UpdateHealth(Image HealthBar, int Health)
    {
        HealthBar.color = Color.Lerp(Color.green, Color.red, 1 - Health * 0.01f);
        HealthBar.rectTransform.localScale = new Vector3(1 * Health * 0.01f, 1, 1);
    }

}
