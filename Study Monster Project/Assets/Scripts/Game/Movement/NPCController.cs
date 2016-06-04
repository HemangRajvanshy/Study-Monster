using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

[RequireComponent(typeof(WorldObject))]
public class NPCController : CharacterController, IInteractable {

    public List<string> Dialogue = new List<string>();
    public bool RandomMovement = false;
    public float DelayBetweenMovements = 1f;
    public int TilesPerSec = 1;
    public int MoveFreedom = 2;
    public bool AutoTalk = false;
    public bool Combatant = false;

    private Vector2 InitPos; // To keep track of how far the NPC has come from base.
    private int MovementIndex; // Keep track of where player is in following sequence of movements.

    private bool Talking = false;
    private PlayerController Player;
    private Animator _animator;
    [SerializeField]
    private List<BasicMovements> MovementSet = new List<BasicMovements>();
    private enum BasicMovements
    {
        Up,
        Down,
        Right,
        Left,
        TurnRight,
        TurnLeft,
        TurnDown,
        TurnUp,
        Idle
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        onTriggerEnterEvent += TriggerEnter;

        InitPos = transform.position;
        if(MovementSet.Count > 0)
        { StartCoroutine(Movement());  } 
    }

    void TriggerEnter(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player = col.GetComponent<PlayerController>();
            if (AutoTalk)
            {
                Player.Talk(this);
            }
        } 
    }

    public void StopTalking()// Player calls to let npc know that talking is over and that he can get on with business.
    {
        Talking = false;
    }

    public List<string> Interact()
    {
        SetOrientation();
        Talking = true;
        return Dialogue;
    }

    public void FoughtWith()
    {
        Combatant = false;
    }


    private void SetOrientation() // Turn towards the player to talk to him.
    {
        if (moving)
            CancelMove(from);
        if (Player != null)
        {
            if (transform.position.y < Player.transform.position.y)
            {
                Debug.Log("Look UP");
            }
            else if (transform.position.y > Player.transform.position.y)
            {
                Debug.Log("look down");
            }
            else if (transform.position.x > Player.transform.position.x)
            {
                Debug.Log("Look Left");
            }
            else
            {
                Debug.Log("Look Right");
            }
        }
    }

    IEnumerator Movement()
    {
        while (true)
        {
            if (RandomMovement && !Talking)
            {
                PerformMove(RandomMove());
                yield return new WaitForSeconds(DelayBetweenMovements + 1 / TilesPerSec);
            }
            else if(!Talking)
            {
                PerformMove(SequentialMovement());
                yield return new WaitForSeconds(DelayBetweenMovements+1/TilesPerSec);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    protected override IEnumerator Tween(Vector2 deltapos, float waitTime, bool ColCheck = true)
    {
        yield return base.Tween(deltapos, waitTime);
        if (success)
            MovementIndex++; ; // Move was performed, can perform next move now.
    }

    private BasicMovements RandomMove()
    {
        if (!GameManager.Instance.Pause.Paused)
        {
            bool canDo = false;
            BasicMovements mov = BasicMovements.Idle;

            mov = MovementSet[UnityEngine.Random.Range(0, MovementSet.Count)];
            canDo = Mathf.Abs(transform.position.x - InitPos.x) < MoveFreedom && Mathf.Abs(transform.position.y - InitPos.y) < MoveFreedom;
            if(!canDo)
            {
                if(transform.position.x > InitPos.x)
                {
                    return BasicMovements.Left;
                }
                else if(transform.position.x < InitPos.x)
                {
                    return BasicMovements.Right;
                }
                else if(transform.position.y > InitPos.y)
                {
                    return BasicMovements.Down;
                }
                else
                {
                    return BasicMovements.Up;
                }
            }

            return mov;
        }
        else
            return BasicMovements.Idle;
    }

    private BasicMovements SequentialMovement()
    {
        if (!GameManager.Instance.Pause.Paused)
        {
            if (MovementIndex > MovementSet.Count-1)
            {
                MovementIndex = 0;
            }            
            var Res = MovementSet[MovementIndex];
            return Res;
        }
        else
            return BasicMovements.Idle;
    }

    private bool PerformMove(BasicMovements Movement)
    {
        switch (Movement)
        {
            case BasicMovements.Right:
                if (Raycast(Vector2.right, 1f))
                {
                    move(Vector2.right, TilesPerSec);

                    if (transform.localScale.x < 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                    return true;
                }
                break;
            case BasicMovements.Left:
                if (Raycast(Vector2.left, 1f))
                {
                    move(Vector2.left, TilesPerSec);
                    if (transform.localScale.x > 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                    return true;
                }
                break;
            case BasicMovements.Up:
                if (Raycast(Vector2.up, 1f))
                {
                    move(Vector2.up, TilesPerSec);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("RunUP"));
                    return true;
                }
                break;
            case BasicMovements.Down:
                if (Raycast(Vector2.down, 1f))
                {
                    move(Vector2.down, TilesPerSec);
                    
                    if (_animator)
                        _animator.Play(Animator.StringToHash("RunDown"));
                    return true;
                }
                break;
            case BasicMovements.Idle:  
                //Don't move               
                if (_animator)
                    _animator.Play(Animator.StringToHash("Idle"));
                return true;
                break;
        }
        return false;
    }
    
}
