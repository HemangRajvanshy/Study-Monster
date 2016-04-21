using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour {

    public GameObject TextBox;
    public Text Dialogue;

    void Start()
    {
        TextBox.SetActive(false);
    }
}
