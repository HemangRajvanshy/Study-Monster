using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryView : MonoBehaviour {

    public GameObject InventoryPanel;

    private bool visible = false;

    public void ToggleInventoryMenu()
    {
        if (!visible)
        {
            InventoryPanel.SetActive(true);
            visible = true;
        }
        else
        {
            InventoryPanel.SetActive(false);
            visible = false;
        }
    }

}
