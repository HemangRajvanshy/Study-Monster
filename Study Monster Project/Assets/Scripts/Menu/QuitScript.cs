using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitScript : MonoBehaviour {

    public Button QuitButton;

    public void Quit()
    {
        Application.Quit();
    }

    //Cancel Button handeled from Menu_UI

    public void ShowQuitPanel()
    {
        QuitButton.interactable = false;
        this.gameObject.SetActive(true);
    }

    public void HideQuitPanel()
    {
        QuitButton.interactable = true;
        this.gameObject.SetActive(false);
    }
}
