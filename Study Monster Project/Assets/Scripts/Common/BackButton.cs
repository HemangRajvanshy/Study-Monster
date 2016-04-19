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
                menu = GameObject.Find("Canvas/Menu_UI").GetComponent<Menu_UI>();
            menu.Back();
        }
        else
        {
            GameManager.Instance.OnBack();
        }
    }

}
