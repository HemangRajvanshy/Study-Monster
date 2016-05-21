using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour { //Enables and disables the Room gameobject and also listens to the event.

    public GameObject RoomObject;
    public GameObject PortalDoorOut;
    public GameObject PortalDoorIn;

    private PlayerController PlayerControl;
    private Vector2 TeleportTo;
    private Vector2 TeleportBackTo;
    private bool IsIn = false;

    void Start()
    {
        TeleportTo = PortalDoorOut.transform.position;
        TeleportBackTo = PortalDoorIn.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PlayerControl = col.GetComponent<PlayerController>();
            if (IsIn)
                StartCoroutine(GoOut(col.transform));
            else
                StartCoroutine(GoIn(col.transform));
        }
    }

    private IEnumerator GoIn(Transform player)
    {
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        Debug.Log(PlayerControl.moving);
        PlayerControl.move(Vector2.up, PlayerControl.TilesPerSecond, false);
        while (PlayerControl.moving)
        {
            Debug.Log("Moving: " + player.position);
            yield return new WaitForEndOfFrame();
        }
        PlayerControl.Teleport(TeleportTo); //teleport camera to new location to effectively hide the background.
        IsIn = true;
    }

    private IEnumerator GoOut(Transform player)
    {
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        PlayerControl.move(Vector2.down, PlayerControl.TilesPerSecond, false);
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        PlayerControl.Teleport(TeleportBackTo);
        IsIn = false;
    }

}
