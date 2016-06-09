using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HealingInteractable : Interactable {

	public override List<string> Interact()
    {
        StartCoroutine(HealPlayer());
        return base.Interact();
    }

    private IEnumerator HealPlayer()
    {
        var CameraControl = Camera.main.GetComponent<CameraController>();
        InputManager.CanReadInput = false;

        GameManager.Instance.BattleManager.Player.HealDamage(GameManager.Instance.BattleManager.Player.TotalHealth - GameManager.Instance.BattleManager.Player.GetHealth());
        yield return new WaitForSeconds(0.3f);
        
        //PLAY SFXX
        //SFX


        //FadeIn Out And Teleport
        StartCoroutine(CameraControl.FadeIn(0.5f, 0.05f));
        while (CameraControl.fading)
            yield return new WaitForEndOfFrame();
        StartCoroutine(CameraControl.FadeOut(0.5f, 0.05f));

        while (GameManager.Instance.TextType.Typing)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.6f);

        GameManager.Instance.GameUI.Dialogue.Say(base.Dialogue);
        GameManager.Instance.Player.StopTalking();

        InputManager.CanReadInput = true;
    }
}
