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
    Coroutine InputRoutine;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PlayerControl = col.GetComponent<PlayerController>();
            InputRoutine = StartCoroutine(ReadEntryInput());
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StopCoroutine(InputRoutine);
        }
    }

    private IEnumerator ReadEntryInput()
    {
        while(true)
        {
            if (!IsIn && Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(GoIn(PlayerControl.transform));
            if (IsIn && Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(GoOut(PlayerControl.transform));
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator GoIn(Transform player)
    {
        IsIn = true;
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        PlayerControl.move(Vector2.up, PlayerControl.TilesPerSecond, false);
        while (PlayerControl.moving)
        {
            yield return new WaitForEndOfFrame();
        }
        PlayerControl.Teleport(TeleportTo); //teleport camera to new location to effectively hide the background.
    }

    private IEnumerator GoOut(Transform player)
    {
        IsIn = false;
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        PlayerControl.move(Vector2.down, PlayerControl.TilesPerSecond, false);
        while (PlayerControl.moving)
            yield return new WaitForEndOfFrame();
        PlayerControl.Teleport(TeleportBackTo);
    }

}
