using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;

    private bool paused;

	void Start ()
    {
        PausePanel.SetActive(false);
        paused = false;
	}

    public void Pause_Resume()
    {
        if (paused)
        {
            paused = false;
            PausePanel.SetActive(false);
        }
        else
        {
            paused = true;
            PausePanel.SetActive(true);
        }
    }

    public void Menu()
    {
        Main.Instance.LoadMenuScene();
    }
}
