using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main : MonoBehaviour {

    public static Main Instance;

    public MusicManager MusicMgr;
    public SfxManager SfxMgr;
    public GameObject LoadScreen;
    public BackButton Back;
    public Player player;
    public SceneLoadManager SceneMgr;

    public PlayerSave PlayerData { get { return player.PlayerData; } }

    #region UnityMethods
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Init();
    }
    #endregion

    #region publicMethods

    public void LoadGameScene()
    {
        SceneMgr.LoadGameScene();
    }

    public void LoadMenuScene()
    {
        SceneMgr.LoadMenuScene();
    }

    public void SavePlayerProgress(int PIndex)
    {
        player.SetProgress(PIndex);
    }

    public void OnClick(UnityEngine.EventSystems.RaycastResult result)
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            GameManager.Instance.OnClick(result);
        }
    }

    #region Loading
    private AsyncOperation async;

    public void ShowLoadingScreen(string Level)
    {
        LoadScreen.SetActive(true);
        StartCoroutine(WaitToLoad(Level));
    }

    IEnumerator WaitToLoad(string Level)
    {
        async = SceneManager.LoadSceneAsync(Level);
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        LoadScreen.SetActive(false);
    }
    #endregion

    #endregion

    #region privateMethods

    private void Init()
    {
        player.Init(); // Load the game from save.

        MusicMgr.Initialize(); // Setup Audio
        SfxMgr.Initialize();
    }

    #endregion
}
