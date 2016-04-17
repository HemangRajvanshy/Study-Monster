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

    public void OnClick()
    {
        if (GameManager.Instance.TextType.Typing)
        {
            GameManager.Instance.TextType.StopTyping();
        }
        else
        {
            Next();
        }
    }

    private void Next() //Next Dialogue
    {
        if ((CurrentScene.Dialogues.Count - 1) > DialogueNumber) //Check if there are any more dialogues.
        {
            DialogueNumber++;
            NextDialogue(DialogueNumber);
        }
        else
        {
            if ((CurrentStory.StoryScenes.Length - 1) > SceneNumber) // Check if there are any more Story Scenes.
            {
                NextScene();
                if (CurrentStory.StoryScenes.Length - 2 == SceneNumber)
                {
                    LastScene();
                }
            }
            else
            {
                Close();
            }
        }
    }

    private void NextDialogue(int DialogueNmuber)
    {
        if (CurrentScene.SfxList[DialogueNumber] != null)
            Main.Instance.SfxMgr.Play(CurrentScene.SfxList[DialogueNumber]);
        StartCoroutine(GameManager.Instance.TextType.TypeText(Dialogue, CurrentScene.Dialogues[DialogueNumber], CurrentScene.Delays[DialogueNumber]));
    }

    private void NextScene()
    {
        SceneNumber++;
        CurrentScene = CurrentStory.StoryScenes[SceneNumber];
        DialogueNumber = 0;
        NextDialogue(DialogueNumber);
        StoryImage.sprite = CurrentScene.image;
    }

    private void LastScene()
    {
        Debug.Log("Last Scene, might as well start loading Game or somethng. Delete if not needed.");
    }

    private void Close()
    {
        StoryPanel.SetActive(false);
        GameManager.Instance.StoryComplete();
        Debug.Log("Reached End of Story");
    }
}
