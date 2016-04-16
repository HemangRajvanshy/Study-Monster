using UnityEngine;
using System.Collections;

public class PlayPanel : MonoBehaviour {

    public Menu_UI Menu;

    public void Play()
    {
        Menu.ShowSaveSelect();
        //Main.Instance.LoadGameScene();
    }

    public void ShowPlayPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePlayPanel()
    {
        this.gameObject.SetActive(false);
    }
}
