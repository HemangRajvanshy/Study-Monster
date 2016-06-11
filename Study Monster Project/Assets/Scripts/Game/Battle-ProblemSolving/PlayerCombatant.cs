using UnityEngine;
using System.Collections;

public class PlayerCombatant : StudyCombatant {

    protected override void Start()
    {
        base.Start();
        if(Main.Instance.player.GameData.PlayerHealth != 0)
            Health = Main.Instance.player.GameData.PlayerHealth;
    }

	public void Lost()
    {
        Debug.Log("TODO: LOST");
    }
}
