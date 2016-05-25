using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class InputManager : MonoBehaviour {
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Main.Instance.Back.Back();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
                Main.Instance.OnClick(raycastResults[0]);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (Main.Instance.SceneMgr.IsSceneLoaded("Game") && !GameManager.Instance.Pause.Paused)
            {
                GameManager.Instance.Inventory.ToggleInventoryMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) // KEY CODE TO BE CHANGED
        {
            if(Main.Instance.SceneMgr.IsSceneLoaded("Game"))
            {
                GameManager.Instance.Player.HandleInteraction();
            }
        }
    }
}
