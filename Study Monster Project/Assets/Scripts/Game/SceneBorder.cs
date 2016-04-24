using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneBorder : MonoBehaviour {

    public string SceneToLoad = "";

    private string PreviousActive = "";

    private SceneLoadManager SceneMgr;

    void Start()
    {
        SceneMgr = Main.Instance.SceneMgr;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(SceneToLoad != "" && !Main.Instance.SceneMgr.IsSceneLoaded(SceneToLoad))
            {
                PreviousActive = SceneMgr.ActiveScene;
                SceneMgr.LoadAdditiveScene(SceneToLoad);
                CheckUnload();
            }
        }
    }

    private void CheckUnload()
    {
        foreach(string Scene in SceneMgr.LoadedAdditives)
        {
            if(Scene != PreviousActive && Scene != "Game" && Scene != SceneToLoad)
            {
                SceneMgr.UnloadAdditiveScene(Scene);
            }
        }
    }
}
