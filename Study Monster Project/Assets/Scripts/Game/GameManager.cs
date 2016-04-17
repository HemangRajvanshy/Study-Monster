using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Pause Pause;
    public TextType TextType;
    public StoryManager StoryManager;

    private int ProgressIndex;

    #region UnityMethods

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Start()
    {
        ProgressIndex = Main.Instance.player.GameData.ProgressIndex;
        if (ProgressIndex == 0)
            IncrementProgress();
    }

    #endregion

    #region Public Methods

    public void OnBack()
    {
        Pause.Pause_Resume();
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
            //Start Story
        }
    }

    private void SaveGame()
    {
        Main.Instance.player.SaveGame();
    }

    #endregion
}
