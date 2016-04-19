using UnityEngine;
using System.Collections;

public class PlayerController : CharacterController {

    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster

    private float normalizedHorizontalSpeed = 0;
    private float normalizedVerticalSpeed = 0;

    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;

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
                normalizedHorizontalSpeed = 1;
                if (transform.localScale.x < 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_animator)
                    _animator.Play(Animator.StringToHash("Run"));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                normalizedHorizontalSpeed = -1;
                if (transform.localScale.x > 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_animator)
                    _animator.Play(Animator.StringToHash("Run"));
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                normalizedVerticalSpeed = 1;

                if (_animator)
                    _animator.Play(Animator.StringToHash("RunUP"));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                normalizedVerticalSpeed = -1;

                if (_animator)
                    _animator.Play(Animator.StringToHash("RunDown"));
            }
            else
            {
                normalizedHorizontalSpeed = 0;
                normalizedVerticalSpeed = 0;

                if (_animator)
                    _animator.Play(Animator.StringToHash("Idle"));
            }

            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * groundDamping);
            _velocity.y = Mathf.Lerp(_velocity.y, normalizedVerticalSpeed * runSpeed, Time.deltaTime * groundDamping);

            move(_velocity * Time.deltaTime);

            _velocity = velocity; //Get current velocity from Chara Controller   
        }   
    }
}
