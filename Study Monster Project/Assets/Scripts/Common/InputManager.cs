using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Main.Instance.Back.Back();
        }
    }
}
