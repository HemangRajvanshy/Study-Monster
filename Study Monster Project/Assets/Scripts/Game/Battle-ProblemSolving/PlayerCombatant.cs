using UnityEngine;
using System.Collections;

public class PlayerCombatant : StudyCombatant {

    public PlayerController PlayerControl;

    protected override void Start()
    {
        base.Start();
        if(Main.Instance.player.GameData.PlayerHealth != 0)
            Health = Main.Instance.player.GameData.PlayerHealth;
    }

	public void Lost()
    {
        Debug.Log("TODO Effects");
        StartCoroutine(StartLoseCycle());        
    }

    private IEnumerator StartLoseCycle()
    {
        yield return new WaitForSeconds(0.2f);

        var CameraControl = Camera.main.GetComponent<CameraController>();
        InputManager.CanReadInput = false;

        //Fade In Out and teleport player
        StartCoroutine(CameraControl.FadeIn(0.5f, 0.05f));
        while (CameraControl.fading)
            yield return new WaitForEndOfFrame();
        StartCoroutine(CameraControl.FadeOut(0.5f, 0.05f));

        HealingInteractable Healpoint = Main.Instance.SceneMgr.ActiveSceneParam.SceneHealLocation;
        PlayerControl.Teleport(Healpoint.Location);
        PlayerControl.Turn(Healpoint.direction, PlayerControl.PlayerSprite, this.GetComponent<Animator>());
        
        yield return new WaitForSeconds(0.5f);

        InputManager.CanReadInput = true;

        PlayerControl.HandleInteraction();
    }
}
