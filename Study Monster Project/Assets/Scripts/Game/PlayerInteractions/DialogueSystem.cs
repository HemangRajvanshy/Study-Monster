using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DialogueSystem : MonoBehaviour {

    public GameObject TextBox;
    public Text Dialogue;

    private TextType TextType;
    private int DialogueIndex = 0;

    void Start()
    {
        TextBox.SetActive(false);
        TextType = GameManager.Instance.TextType;
    }

    public bool Say(List<string> dialogues)
    {
        if (!TextType.Typing)
        {
            if (DialogueIndex < dialogues.Count)
            {
                TextBox.SetActive(true);
                StartCoroutine(TextType.TypeText(Dialogue, dialogues[DialogueIndex]));
                DialogueIndex++;
                return true; 
            }
            else
            {
                //Talking has concluded. Disable the text box and tell player to get going. Also reset for use again.
                TextBox.SetActive(false);
                DialogueIndex = 0;
                return false;
            }
        }
        else
        {
            TextType.StopTyping();
        }
   
        return true;
    }

    public int GetDialogueIndex()
    {
        return DialogueIndex;
    }

    private IEnumerator WaitWhileTyping()
    {
        yield return new WaitForEndOfFrame();
    }
}
