using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;

    public bool Paused { get { return paused; } }

    private bool paused;

	void Start ()
    {
        PausePanel.SetActive(false);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
	}

    public void Pause_Resume()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
        }
    }

    public void Save()
    {
        Main.Instance.player.SaveGame();
        Debug.Log("Game Saved.");
    }

    public void Menu()
    {
        Main.Instance.LoadMenuScene();
    }
}
