using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Pause Pause;

    #region UnityMethods

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    #endregion

    #region Public Methods

    public void OnBack()
    {
        Pause.Pause_Resume();
    }

    #endregion
}
