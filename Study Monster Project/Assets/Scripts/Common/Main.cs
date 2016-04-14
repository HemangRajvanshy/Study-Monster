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

    }
    #endregion


}
