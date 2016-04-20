using UnityEngine;
using System;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public class CharacterCollisionState2D
    {
        public bool right;
        public bool left;
        public bool above;
        public bool below;

        public void reset()
        {
            right = left = above = below = false;
        }
    }

    public event Action<RaycastHit2D> onControllerCollidedEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action<Collider2D> onTriggerStayEvent;
    public event Action<Collider2D> onTriggerExitEvent;

    [HideInInspector]
    [NonSerialized]
    public new Transform transform;
    [HideInInspector]
    [NonSerialized]
    public BoxCollider2D boxCollider;

    [HideInInspector]
    [NonSerialized]
    public CharacterCollisionState2D collisionState = new CharacterCollisionState2D();

    private bool moving;

    #region MonoBehaviour

    protected void Awake()
    {
        moving = false;

        transform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (onTriggerEnterEvent != null)
            onTriggerEnterEvent(col);
    }


    public void OnTriggerStay2D(Collider2D col)
    {
        if (onTriggerStayEvent != null)
            onTriggerStayEvent(col);
    }


    public void OnTriggerExit2D(Collider2D col)
    {
        if (onTriggerExitEvent != null)
            onTriggerExitEvent(col);
    }

    #endregion

    #region Public

    public void move(Vector2 deltaPosition, float TilesPerSec = 1f)
    {
        if (!moving)
        {
            moving = true;
            transform.position += new Vector3(deltaPosition.x, deltaPosition.y);
            StartCoroutine(WaitTillNextMove(1/TilesPerSec));
        }
    }

    #endregion

    IEnumerator WaitTillNextMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }
}