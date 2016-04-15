using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackButton : MonoBehaviour
{
    private Menu_UI menu;

    public void Back()
    {
        if (SceneManager.GetActiveScene().name == "Menu") // Menu Scene
        {
            if (!menu)
                menu = GameObject.Find("Canvas/BackgroundImage").GetComponent<Menu_UI>();
            menu.Back();
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            GameManager.Instance.OnBack();
        }
    }

}
