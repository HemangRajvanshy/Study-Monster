using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    void Awake()
    {
        Camera.main.orthographicSize = Screen.height / 128f;
    }
}
