using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DialogueSystem : MonoBehaviour {

    public GameObject TextBox;
    public Text Dialogue;

    private TextType TextType;

    void Start()
    {
        TextBox.SetActive(false);
        TextType = GameManager.Instance.TextType;
    }

    public void Say(List<string> dialogues)
    {
        if (!TextType.Typing)
        {
            TextBox.SetActive(true);
            StartCoroutine(TextType.TypeText(Dialogue, dialogues[0]));
        }
    }

    private IEnumerator WaitWhileTyping()
    {
        yield return new WaitForEndOfFrame();
    }
}
