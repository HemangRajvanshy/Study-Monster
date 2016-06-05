using UnityEngine;
using System.Collections;

public class PlayerController : CharacterController {

    public int TilesPerSecond = 1;
    public GameObject PlayerSprite;
    public PlayerInventory Inventory;

    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private IInteractable InteractingWith;
    private GameObject InteractingNPC;

    private bool Talking = false;
    private bool Fighting = false;

    new void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _animator.speed = TilesPerSecond * 0.6f;

        // listen to some events for illustration purposes
        onControllerCollidedEvent += onControllerCollider;
        onTriggerEnterEvent += TriggerEnterEvent;
        onTriggerExitEvent += TriggerExitEvent;
    }


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        //if (hit.normal.y == 1f)
        //    return;
        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }

    void TriggerEnterEvent(Collider2D col)
    {
        if (col.gameObject.GetComponent<IInteractable>() != null)
        {
            InteractingWith = col.gameObject.GetComponent<IInteractable>();
            if (col.gameObject.GetComponent<NPCController>() != null)
                InteractingNPC = col.gameObject;
        }
    }

    void TriggerExitEvent(Collider2D col)
    {
        if (col.gameObject.GetComponent<IInteractable>() != null && col.gameObject.GetComponent<IInteractable>() == InteractingWith)
        {
            InteractingWith = null;
            if (col.gameObject.GetComponent<NPCController>() != null && col.gameObject.GetComponent<NPCController>() == InteractingNPC)
                InteractingNPC = null;
        }
    }

    #endregion


    void Update()
    {
        if (!GameManager.Instance.Pause.Paused)
        {
            ReadInput();
        }   
    }

    public void StopFigting()
    {
        Fighting = false;
    }

    public void Teleport(Vector2 To)
    {
        if (moving)
            moving = false;
        transform.position = To;
    }

    public void Talk(IInteractable Interactable)
    {
        Talking = GameManager.Instance.GameUI.Dialogue.Say(Interactable.Interact());
        if(!Talking && InteractingNPC != null)
        {
            if (InteractingNPC.GetComponent<NPCController>().Combatant) // Check whether NPC is combatant. Also somehow check if we have already fought before or not.
            {
                //Start Fighting!
                Fighting = true;
                GameManager.Instance.StartCombat(InteractingNPC.GetComponent<EnemyCombatant>());
            }
            else
            {
                InteractingNPC.GetComponent<NPCController>().StopTalking();
            }
        }
    }

    //public void TalkAfterFight(EnemyCombatant enemy, bool win)
    //{
    //    if(win)
    //    {
    //        Talking = GameManager.Instance.Dialogue.Say(enemy.AfterLooseDialogue);
    //    }
    //    else
    //    {
    //        Talking = GameManager.Instance.Dialogue.Say(enemy.AfterWinDialogue);
    //    }
    //}

    public void HandleInteraction()
    {
        if (!moving && !Fighting && !GameManager.Instance.Pause.Paused)
        {
            if (InteractingWith != null)
            {
                Talk(InteractingWith);
            } 
        }
    }

    private void ReadInput()
    {
        if (!Talking && !Fighting && InputManager.CanReadInput)
        {
            if (Input.GetKey(KeyCode.RightArrow) && Raycast(Vector2.right, 1f))
            {
                HandleMovement(KeyCode.RightArrow, Vector2.right, 3);
                Turn(3, PlayerSprite, _animator);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Raycast(Vector2.left, 1f))
            {
                HandleMovement(KeyCode.LeftArrow, Vector2.left, 2);
                Turn(2, PlayerSprite, _animator);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Raycast(Vector2.up, 1f))
            {
                HandleMovement(KeyCode.UpArrow, Vector2.up, 1);
                Turn(1, PlayerSprite, _animator);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Raycast(Vector2.down, 1f))
            {
                HandleMovement(KeyCode.DownArrow, Vector2.down, 0);
                Turn(0, PlayerSprite, _animator);
            }
            else
            {
                if(!moving)             
                    _animator.SetFloat("Speed", 0f);
            } 
        }
    }

    public void HandleMovement(KeyCode pressed, Vector2 Direction, int direction, bool ColCheck = true)
    {
        move(Direction, TilesPerSecond, ColCheck);
        Turn(direction, PlayerSprite, _animator);
        _animator.SetFloat("Speed", 1f);
    }

}
