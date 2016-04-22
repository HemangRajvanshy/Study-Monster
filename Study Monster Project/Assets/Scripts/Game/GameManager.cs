using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Pause Pause;
    public TextType TextType;
    public DialogueSystem Dialogue;
    public StoryManager StoryManager;

    private int ProgressIndex;
    private GameState state;

    private enum GameState
    {
        Story,
        Game,
        Talking
    };

    #region UnityMethods

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Start()
    {
        state = GameState.Game;
        ProgressIndex = Main.Instance.player.GameData.ProgressIndex;
        int SceneLocation = Main.Instance.player.GameData.SceneLocation;
        if (ProgressIndex == 0)
            IncrementProgress();
        Main.Instance.SceneMgr.LoadAdditiveScene("01-Start"); Debug.Log("TODO: Save scene location and load from it");
    }

    #endregion

    #region Public Methods

    public void OnBack()
    {
        Pause.Pause_Resume();
    }

    public void StoryComplete()
    {
        state = GameState.Game;
        if(ProgressIndex == 1)
        {
            IncrementProgress();
        }
    }

    public void OnClick(UnityEngine.EventSystems.RaycastResult ray)
    {
        if (ray.module.name == "PauseCanvas")
            return;
        switch (state)
        {
            case GameState.Story:
                StoryManager.OnClick();
                break;
            case GameState.Game:
                break;
        }
    }

    #endregion

    #region Private Methods

    private void IncrementProgress()
    {
        ProgressIndex++;
        Main.Instance.player.SetProgress(ProgressIndex);
        SaveGame();
        ProgressTriggers();
    }

    private void ProgressTriggers() // Trigger an event at some progress number
    {
        if(ProgressIndex == 1)
        {
            StartStory(0);
        }
    }

    private void SaveGame()
    {
        Main.Instance.player.SaveGame();
    }

    private void StartStory(int StoryIndx)
    {
        state = GameState.Story;
        StoryManager.Init(StoryIndx);
    }

    #endregion
}
