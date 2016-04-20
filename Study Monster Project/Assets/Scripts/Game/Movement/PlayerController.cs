using UnityEngine;
using System.Collections;

public class PlayerController : CharacterController {

    public int TilesPerSecond = 1;

    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;

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
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void TriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void TriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion


    void Update()
    {
        if (!GameManager.Instance.Pause.Paused)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move(Vector2.right, TilesPerSecond);

                if (transform.localScale.x < 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_animator)
                    _animator.Play(Animator.StringToHash("Run"));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                move(Vector2.left, TilesPerSecond);

                if (transform.localScale.x > 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_animator)
                    _animator.Play(Animator.StringToHash("Run"));
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                move(Vector2.up, TilesPerSecond);

                if (_animator)
                    _animator.Play(Animator.StringToHash("RunUP"));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
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
}
