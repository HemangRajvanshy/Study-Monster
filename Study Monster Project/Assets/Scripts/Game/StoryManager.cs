using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class StoryManager : MonoBehaviour {

    public List<Story> Stories;

    public GameObject StoryPanel;
    public Image StoryImage;
    public Text Dialogue;

    private Story CurrentStory;
    private int SceneNumber;
    private int DialogueNumber;
    private StoryScene CurrentScene;

    public void Init(int StoryNumber) // Called by GameManager
    {
        StoryPanel.SetActive(true);
        CurrentStory = Stories[StoryNumber];
        SceneNumber = 0;
        DialogueNumber = 0;

        CurrentScene = CurrentStory.StoryScenes[SceneNumber];
        StoryImage.sprite = CurrentScene.image;
        NextDialogue(DialogueNumber);
    }

    private void NextDialogue(int DialogueNmuber)
    {
        if (CurrentScene.SfxList[DialogueNumber] != null)
            Main.Instance.SfxMgr.Play(CurrentScene.SfxList[DialogueNumber]);
        StartCoroutine(GameManager.Instance.TextType.TypeText(Dialogue, CurrentScene.Dialogues[DialogueNumber], CurrentScene.Delays[DialogueNumber]));
    }
}
