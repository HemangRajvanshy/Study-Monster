using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

//Load and also unload Manager.
public class SceneLoadManager : MonoBehaviour {

    public string ActiveScene { get { return SceneManager.GetActiveScene().name; } }
    public SceneParam ActiveSceneParam { get { return SceneManager.GetActiveScene().GetRootGameObjects()[0].GetComponent<SceneParam>();  } }
    public List<string> LoadedAdditives = new List<string>();

    private Scene PrevActive;

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
    public void LoadAdditiveScene(string sceneName, SceneBorder.BorderPosition borderPos = SceneBorder.BorderPosition.None)
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        LoadedAdditives.Add(sceneName);
        StartCoroutine(WaitToLoad(sceneName, borderPos));
    }

    IEnumerator WaitToLoad(string Level, SceneBorder.BorderPosition borderPos = SceneBorder.BorderPosition.None)
    {
        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        PrevActive = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Level));
        if(borderPos != SceneBorder.BorderPosition.None)
            SetScenePosition(borderPos);
    }

    public void SetScenePosition(SceneBorder.BorderPosition borderPos)
    {
        GameObject SceneObject = SceneManager.GetActiveScene().GetRootGameObjects()[0];
        GameObject PrevSceneObject = PrevActive.GetRootGameObjects()[0];
        SceneParam SceneParm = SceneObject.GetComponent<SceneParam>();
        SceneParam PrevSceneParm = PrevSceneObject.GetComponent<SceneParam>();

        switch (borderPos)
        {
            case SceneBorder.BorderPosition.top:
                SceneObject.transform.position = new Vector3(0, (-SceneParm.BottomBorder.transform.position.y+PrevSceneParm.TopBorder.transform.position.y), 0);
                break;
            case SceneBorder.BorderPosition.bottom:
                SceneObject.transform.position = new Vector3(0, -(SceneParm.TopBorder.transform.position.y - PrevSceneParm.BottomBorder.transform.position.y), 0);
                break;
            case SceneBorder.BorderPosition.right:
                SceneObject.transform.position = new Vector3((-SceneParm.LeftBorder.transform.position.x + PrevSceneParm.RightBorder.transform.position.x), 0, 0);
                break;
            case SceneBorder.BorderPosition.left:
                SceneObject.transform.position = new Vector3(-(SceneParm.RightBorder.transform.position.x - PrevSceneParm.LeftBorder.transform.position.x), 0, 0);
                break;
        }
    }

    public void UnloadAdditiveScene(string sceneName)
    {
        StartCoroutine(WaitToUnload(sceneName));
    }

    IEnumerator WaitToUnload(string SceneName)
    {
        yield return new WaitForSeconds(0.1f);
        while (!SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            Debug.Log("Not Loaded");
            yield return new WaitForEndOfFrame();
        }
        SceneManager.UnloadScene(SceneName);
        LoadedAdditives.Remove(SceneName);
        Resources.UnloadUnusedAssets();
    }

    public bool IsSceneLoaded(string SceneName)
    {
        return SceneManager.GetSceneByName(SceneName).isLoaded;
    }
}
