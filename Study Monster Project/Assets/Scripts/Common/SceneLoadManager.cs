using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

//Load and also unload Manager.
public class SceneLoadManager : MonoBehaviour {

    public string ActiveScene { get { return SceneManager.GetActiveScene().name; } }
    public List<string> LoadedAdditives = new List<string>();

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
        LoadedAdditives.Add(sceneName);
        StartCoroutine(WaitToLoad(sceneName));
    }

    IEnumerator WaitToLoad(string Level)
    {
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Level));
        SceneManager.GetActiveScene().GetRootGameObjects()[0].transform.position = new Vector3(0, 0, 0);
    }

    public void UnloadAdditiveScene(string sceneName)
    {
        SceneManager.UnloadScene(sceneName);
        LoadedAdditives.Remove(sceneName);
    }

    public bool IsSceneLoaded(string SceneName)
    {
        return SceneManager.GetSceneByName(SceneName).isLoaded;
    }
}
