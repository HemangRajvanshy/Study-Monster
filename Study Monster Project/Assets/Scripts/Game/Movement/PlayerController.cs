﻿using UnityEngine;
using System.Collections;

public class PlayerController : CharacterController {

    public int TilesPerSecond = 1;
    public GameObject PlayerSprite;

    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private IInteractable InteractingWith;
    private GameObject InteractingNPC;

    private bool Talking = false;

    new void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();

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
            if (Input.GetKeyDown(KeyCode.Z)) // KEY CODE TO BE CHANGED
                HandleInteraction();

            if (!Talking)
                HandleMovement();
        }   
    }

    public void Talk(IInteractable Interact)
    {
        Talking = GameManager.Instance.Dialogue.Say(InteractingWith.Interact());
        if(!Talking && InteractingNPC != null)
        {
            if(InteractingNPC.GetComponent<NPCController>().Combatant) // Also somehow check if we have already fought before or not.
            {
                //Start Fighting!
                GameManager.Instance.BattleManager.InitializeCombat(InteractingNPC.GetComponent<EnemyCombatant>());
            }
        }
    }

    private void HandleInteraction()
    {
        if(InteractingWith != null)
        {
            Talk(InteractingWith);
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow) && CheckRight())
        {
            move(Vector2.right, TilesPerSecond);

            if (PlayerSprite.transform.localScale.x < 0f)
                PlayerSprite.transform.localScale = new Vector3(-PlayerSprite.transform.localScale.x, PlayerSprite.transform.localScale.y, PlayerSprite.transform.localScale.z);

            if (_animator)
                _animator.Play(Animator.StringToHash("Run"));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && CheckLeft())
        {
            move(Vector2.left, TilesPerSecond);

            if (PlayerSprite.transform.localScale.x > 0f)
                PlayerSprite.transform.localScale = new Vector3(-PlayerSprite.transform.localScale.x, PlayerSprite.transform.localScale.y, PlayerSprite.transform.localScale.z);

            if (_animator)
                _animator.Play(Animator.StringToHash("Run"));
        }
        else if (Input.GetKey(KeyCode.UpArrow) && CheckUp())
        {
            move(Vector2.up, TilesPerSecond);

            if (_animator)
                _animator.Play(Animator.StringToHash("RunUP"));
        }
        else if (Input.GetKey(KeyCode.DownArrow) && CheckDown())
        {
            move(Vector2.down, TilesPerSecond);

            if (_animator)
                _animator.Play(Animator.StringToHash("RunDown"));
        }
        else
        {
            //Don't move               
            if (_animator)
                _animator.Play(Animator.StringToHash("Idle"));
        }
    }



}
