using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
         // Making it so that I can pass it to CharController.move()
        _animator = GetComponent<Animator>();
        onTriggerEnterEvent += TriggerEnter;

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
        if(Player != null)
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
                yield return new WaitForSeconds(DelayBetweenMovements);
            }
            else if(!Talking)
            {
                PerformMove(SequentialMovement());
                yield return new WaitForSeconds(DelayBetweenMovements);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private BasicMovements RandomMove()
    {
        if (!GameManager.Instance.Pause.Paused)
        {

        }
        else
            return BasicMovements.Idle;

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
            MovementIndex++;
            return Res;
        }
        else
            return BasicMovements.Idle;
    }

    private void PerformMove(BasicMovements Movement)
    {
        switch (Movement)
        {
            case BasicMovements.Right:
                if (CheckRight())
                {
                    move(Vector2.right, 1/TilesPerSec);

                    if (transform.localScale.x < 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                }
                break;
            case BasicMovements.Left:
                if (CheckLeft())
                {
                    move(Vector2.left, 1 / TilesPerSec);
                    if (transform.localScale.x > 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                }
                break;
            case BasicMovements.Up:
                if (CheckUp())
                {
                    move(Vector2.up, 1 / TilesPerSec);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("RunUP"));
                }
                break;
            case BasicMovements.Down:
                if (CheckDown())
                {
                    move(Vector2.down, 1 / TilesPerSec);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("RunDown"));
                }
                break;
            case BasicMovements.Idle:
                //Don't move               
                if (_animator)
                    _animator.Play(Animator.StringToHash("Idle"));
                break;
        }

    }
    
}
