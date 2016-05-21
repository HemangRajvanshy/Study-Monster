using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorScript : MonoBehaviour { //Enables and disables the Room gameobject and also listens to the event.

    public GameObject RoomObject;

    private bool IsIn = false;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (IsIn)
                GoOut();
            else
                GoIn();
        }
    }

    private void GoIn()
    {
        RoomObject.SetActive(true); // Maybe also teleport camera to new location to effectively hide the background.
        IsIn = true;
    }

    private void GoOut()
    {
        RoomObject.SetActive(false);
        IsIn = false;
    }

}
