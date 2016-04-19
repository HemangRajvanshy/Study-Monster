using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;

    public bool Paused { get { return paused; } }

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
            Time.timeScale = 0f;
            PausePanel.SetActive(false);
        }
        else
        {
            paused = true;
            Time.timeScale = 1f;
            PausePanel.SetActive(true);
        }
    }

    public void Menu()
    {
        Main.Instance.LoadMenuScene();
    }
}
