using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCController : CharacterController, IInteractable {

    public List<string> Dialogue = new List<string>();
    public bool RandomMovement = false;
    public float DelayBetweenMovements = 1f;
    public int MoveFreedom = 2;
    public bool AutoTalk = false;
    public bool Combatant = false;

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
        DelayBetweenMovements = 1 / DelayBetweenMovements; // Making it so that I can pass it to CharController.move()
        _animator = GetComponent<Animator>();
        onTriggerEnterEvent += TriggerEnter;
    }

    void TriggerEnter(Collider2D col)
    {
        if(AutoTalk)
        {
            //talk
        }
    }

    public List<string> Interact()
    {
        return Dialogue;
    }

    private void StartMovement()
    {

    }

    private void PerformMove(BasicMovements Movement)
    {
        switch (Movement)
        {
            case BasicMovements.Right:
                if (CheckRight())
                {
                    move(Vector2.right, DelayBetweenMovements);

                    if (transform.localScale.x < 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                }
                break;
            case BasicMovements.Left:
                if (CheckLeft())
                {
                    move(Vector2.left, DelayBetweenMovements);

                    if (transform.localScale.x > 0f)
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("Run"));
                }
                break;
            case BasicMovements.Up:
                if (CheckUp())
                {
                    move(Vector2.up, DelayBetweenMovements);

                    if (_animator)
                        _animator.Play(Animator.StringToHash("RunUP"));
                }
                break;
            case BasicMovements.Down:
                if (CheckDown())
                {
                    move(Vector2.down, DelayBetweenMovements);

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
