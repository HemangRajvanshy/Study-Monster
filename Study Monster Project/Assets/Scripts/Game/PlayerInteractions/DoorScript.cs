using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour { //Enables and disables the Room gameobject and also listens to the event.

    public GameObject RoomObject;
    public GameObject PortalDoorOut;
    public GameObject PortalDoorIn;
    public Collider2D HouseCollider;

    private CameraController CameraControl;
    private PlayerController PlayerControl;
    private Vector2 TeleportTo;
    private Vector2 TeleportBackTo;
    private bool IsIn = false;
    private bool ReadInput = false;

    void Start()
    {
        CameraControl = Camera.main.GetComponent<CameraController>(); 
        TeleportTo = PortalDoorOut.transform.position;
        TeleportBackTo = new Vector2(PortalDoorIn.transform.position.x, PortalDoorIn.transform.position.y + 1);
    }

    void Update()
    {
        if (ReadInput)
        {
            if (!IsIn && Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(GoIn(PlayerControl.transform));
            if (IsIn && Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(GoOut(PlayerControl.transform));
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            PlayerControl = col.GetComponent<PlayerController>();
            ReadInput = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ReadInput = false;
        }
    }



    private IEnumerator GoIn(Transform player)
    {
        if (InputManager.CanReadInput)
        {
            InputManager.CanReadInput = false;
            IsIn = true;
            while (PlayerControl.moving)
                yield return new WaitForEndOfFrame();
            PlayerControl.HandleMovement(KeyCode.UpArrow, Vector2.up, 1, false);
            while (PlayerControl.moving)
            {
                yield return new WaitForEndOfFrame();
            }

            //FadeIn Out And Teleport
            StartCoroutine(CameraControl.FadeIn(0.3f, 0.05f));
            while (CameraControl.fading)
                yield return new WaitForEndOfFrame();
            StartCoroutine(CameraControl.FadeOut(0.3f, 0.05f));
            PlayerControl.Teleport(TeleportTo); //teleport camera to new location to effectively hide the background.

            InputManager.CanReadInput = true; 
        }
    }

    private IEnumerator GoOut(Transform player)
    {
        if (InputManager.CanReadInput)
        {
            InputManager.CanReadInput = false;

            IsIn = false;
            HouseCollider.enabled = false;
            while (PlayerControl.moving)
                yield return new WaitForEndOfFrame();

            PlayerControl.Turn(0, PlayerControl.PlayerSprite, PlayerControl.GetComponent<Animator>());

            //FadeInOut And teleport
            StartCoroutine(CameraControl.FadeIn(0.3f, 0.05f));
            while (CameraControl.fading)
                yield return new WaitForEndOfFrame();
            PlayerControl.Teleport(TeleportBackTo);
            StartCoroutine(CameraControl.FadeOut(0.3f, 0.05f));
            while (CameraControl.fading)
                yield return new WaitForEndOfFrame();

            PlayerControl.HandleMovement(KeyCode.DownArrow, Vector2.down, 0, false);
            while (PlayerControl.moving)
                yield return new WaitForEndOfFrame();

            HouseCollider.enabled = true;
            InputManager.CanReadInput = true; 
        }
    }

}
