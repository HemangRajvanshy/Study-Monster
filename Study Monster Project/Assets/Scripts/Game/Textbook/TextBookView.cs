using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class TextBookView : MonoBehaviour {

    public List<Sprite> TextbookPages;
    public GameObject TextBookPanel;
    public Button LastPageButton;
    public Button NextPageButton;
    public Image ContentImage; // The textbook material on top of generic background.

    private int PageIndex;


    public void ShowTextbook(int page = -1)
    {
        if (page == -1)
            OpenPage(0);
        TextBookPanel.SetActive(true);
    }

    public void HideTextBook()
    {
        TextBookPanel.SetActive(false);
    }

    public void NextPage()
    {
        if (PageIndex != GameManager.Instance.Player.Inventory.GetAvailableText().Count - 1)
        {
            PageIndex++;
            OpenPage(PageIndex);
        }
    }

    public void LastPage()
    {
        if (PageIndex != 0)
        {
            PageIndex--;
            OpenPage(PageIndex);
        }
    }


    public void OpenPage(int page)
    {
        LastPageButton.interactable = true;
        NextPageButton.interactable = true;
        PageIndex = page;
        if (page == 0)
            LastPageButton.interactable = false;
        if (page >= GameManager.Instance.Player.Inventory.GetAvailableText().Count - 1)
            NextPageButton.interactable = false;

        ContentImage.sprite = TextbookPages[page];
    }

}
