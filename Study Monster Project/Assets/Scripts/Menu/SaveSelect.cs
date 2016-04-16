using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSelect : MonoBehaviour {

    public GameObject SaveSelectPanel;
    public Text Save1Text;
    public Text Save2Text;
    public Text Save3Text;

    private bool Save1Exist;
    private bool Save2Exist;
    private bool Save3Exist;

    void Start()
    {
        HideSaveSelect();
        SetupSaves();
    }

    private void SetupSaves()
    {
        if (Main.Instance.PlayerData != null)
        {
            Main.Instance.player.GetSaveStatus(out Save1Exist, out Save2Exist, out Save3Exist);
        }
        else
        {
            Save1Exist = false;
            Save2Exist = false;
            Save3Exist = false;
        }

        Save1Text.text = "New Game";
        Save2Text.text = "New Game";
        Save3Text.text = "New Game";

        if (Save1Exist)
            Save1Text.text = "Save 1";
        if (Save2Exist)
            Save2Text.text = "Save 2";
        if (Save3Exist)
            Save3Text.text = "Save 3";
    }

    public void ShowSaveSelect()
    {
        SaveSelectPanel.gameObject.SetActive(true);
    }

    public void HideSaveSelect()
    {
        SaveSelectPanel.gameObject.SetActive(false);
    }
}
