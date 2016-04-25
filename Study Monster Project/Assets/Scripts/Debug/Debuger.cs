using UnityEngine;
using System.IO;
using System.Collections;

namespace Debugging
{
    public class Debuger : MonoBehaviour
    {
        public bool ClearSaveOnPlay;

        public bool DebugOn = false;

        void Awake()
        {
            if (ClearSaveOnPlay)
            {
                File.Delete(Application.persistentDataPath + "/info.dat");
                File.Delete(Application.persistentDataPath + "/game1.dat");
                File.Delete(Application.persistentDataPath + "/game2.dat");
                File.Delete(Application.persistentDataPath + "/game3.dat");
            }
        }

        //void Start()
        //{
        //    StartCoroutine(Test());
        //}

        //IEnumerator Test()
        //{
        //    Debug.Log("Test Start");
        //    yield return new WaitForSeconds(1f);
        //    Debug.Log("Loading First Scene");
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("02");
        //    yield return new WaitForSeconds(1f);
        //    Debug.Log("Loaded");
        //    Debug.Log("Unloading that scene");
        //    yield return new WaitForSeconds(2.5f);
        //    Debug.Log(UnityEngine.SceneManagement.SceneManager.UnloadScene("02"));
        //    Debug.Log("Success");
        //}

    }
}