using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

//Load and also unload Manager.
public class SceneLoadManager : MonoBehaviour {

    public string ActiveScene { get { return SceneManager.GetActiveScene().name; } } 

    private List<Scene> ActiveScenes;

    public void LoadGameScene()
    {
        Main.Instance.ShowLoadingScreen("Game");
        if (Main.Instance.MusicMgr.GameBgMusic)
            Main.Instance.MusicMgr.Play(Main.Instance.MusicMgr.GameBgMusic);
    }

    public void LoadMenuScene()
    {
        Main.Instance.ShowLoadingScreen("Menu");
        if (Main.Instance.MusicMgr.MenuBgMusic)
            Main.Instance.MusicMgr.Play(Main.Instance.MusicMgr.MenuBgMusic);
    }


    private AsyncOperation async;
    /// <summary>
    /// For scene loads and unloads in game, we will have invisible boundaries with a generic script where I can set the index of the scenes between which 
    /// the boundary belongs.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadAdditiveScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        StartCoroutine(WaitToLoad(sceneName));
    }

    IEnumerator WaitToLoad(string Level)
    {
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Level));

    }

    public void UnloadAdditiveScene(int sceneIndex)
    {
        SceneManager.UnloadScene(sceneIndex);
    }
}
