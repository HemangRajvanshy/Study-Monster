using UnityEngine;
using System.Collections;

public class PlayPanel : MonoBehaviour {

    public void Play()
    {
        Main.Instance.LoadGameScene();
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
