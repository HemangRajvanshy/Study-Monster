using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCItemOutlet : NPCController
{
    public List<int> TextBookPages = new List<int>();
    public List<string> AfterGiveDialogue = new List<string>();
    public int GiveAfterDialogue = 1;

    private bool Given;

    void Start()
    {
        Debug.Log("TODO: Check whether the item is already given or no.");
    }

    public override List<string> Interact()
    {
        if(GameManager.Instance.GameUI.Dialogue.GetDialogueIndex() == GiveAfterDialogue)
        {
            if(!Given)
            {
                foreach(int TextBookPage in TextBookPages)
                    GameManager.Instance.Player.Inventory.AddPageToText(TextBookPage);
            }
        }
        return base.Interact();
    }
    
}

