using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour {

    public Canvas BattleCanvas; 
        
    public PlayerCombatant Player;
    public Image PlayerHealthBar;

    private StudyCombatant EnemyCombatant;
    public Image EnemyHealthBar;
    
    void Start()
    {
        BattleCanvas.enabled = false;
        //var com = new StudyCombatant();
        //InitializeCombat(com);
    }

    public void InitializeCombat(StudyCombatant Combatant)
    {
        BattleCanvas.enabled = true;

        EnemyCombatant = Combatant;
        UpdateHealth(PlayerHealthBar, Player.Health);
        UpdateHealth(EnemyHealthBar, EnemyCombatant.Health);
    }

    private void EndCombat(bool win)
    {

    }

    private void UpdateHealth(Image HealthBar, int Health)
    {
        HealthBar.color = Color.Lerp(Color.green, Color.red, 1 - Health * 0.01f);
        HealthBar.rectTransform.localScale = new Vector3(1 * Health * 0.01f, 1, 1);
    }

}
