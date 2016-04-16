using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu_UI : MonoBehaviour {

    public GameObject PlayPanel;
    public GameObject CreditsPanel;
    public GameObject QuitPanel;
    public GameObject SaveSelect;

    public Toggle SfxToggle;
    public Toggle MusicToggle;

    private enum MenuState
    {
        Play,
        Credits,
        Quit,
        SaveSelect
    };
    private MenuState menuState;

    void Start()
    {
        SfxToggle.isOn = Main.Instance.SfxMgr.On;
        MusicToggle.isOn = Main.Instance.MusicMgr.On;
        menuState = MenuState.Play;
    }

    public void ShowCredits()
    {
        DeactivatePanels();
        menuState = MenuState.Credits;
        CreditsPanel.SetActive(true);
    }


    public void OnQuitClick()
    {
        DeactivatePanels();
        menuState = MenuState.Quit;
        QuitPanel.GetComponent<QuitScript>().ShowQuitPanel();
    }

    public void ShowSaveSelect()
    {
        DeactivatePanels();
        menuState = MenuState.SaveSelect;
        SaveSelect.GetComponent<SaveSelect>().ShowSaveSelect();
    }


    public void ClosePanel()
    {
        DeactivatePanels();
        menuState = MenuState.Play;
        PlayPanel.GetComponent<PlayPanel>().ShowPlayPanel();
    }

    public void Back()
    {
        if(menuState == MenuState.Play)
        {
            OnQuitClick();
        }
        else
        {
            ClosePanel();
        }
    }

    public void MusicOn_Off()
    {
        Main.Instance.MusicMgr.MenuToggle(MusicToggle.isOn);
    }

    public void SfxOn_Off()
    {
        Main.Instance.SfxMgr.MenuToggle(SfxToggle.isOn);
    }

    private void DeactivatePanels()
    {
        switch (menuState)
        {
            case MenuState.Play:
                PlayPanel.GetComponent<PlayPanel>().HidePlayPanel();
                break;
            case MenuState.Quit:
                QuitPanel.GetComponent<QuitScript>().HideQuitPanel();
                break;
            case MenuState.Credits:
                CreditsPanel.SetActive(false);
                break;
            case MenuState.SaveSelect:
                SaveSelect.GetComponent<SaveSelect>().HideSaveSelect();
                break;
        }
    }
}
