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
        //var com = new StudyCombatant();
        //InitializeCombat(com);
        Debug.Log("x\xB2");
    }

    public void InitializeCombat(EnemyCombatant Combatant)
    {
        BattleCanvas.enabled = true;

        EnemyCombatant = Combatant;
        UpdateHealth(PlayerHealthBar, Player.Health);
        UpdateHealth(EnemyHealthBar, EnemyCombatant.Health);
    }

    public void UpdateCombat(bool Correct)
    {

    }

    private void EndCombat(bool win)
    {
        BattleCanvas.enabled = false;
    }

    private void UpdateHealth(Image HealthBar, int Health)
    {
        HealthBar.color = Color.Lerp(Color.green, Color.red, 1 - Health * 0.01f);
        HealthBar.rectTransform.localScale = new Vector3(1 * Health * 0.01f, 1, 1);
    }

}
