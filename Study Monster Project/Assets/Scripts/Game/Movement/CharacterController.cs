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
    public CharacterCollisionState2D collisionState = new CharacterCollisionState2D();

    private bool moving;
    private string SortingLayer;

    #region MonoBehaviour

    protected void Awake()
    {
        moving = false;

        transform = GetComponent<Transform>();
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

    public void move(Vector2 deltaPosition, float TilesPerSec, Func<bool> check)
    {
        if (!moving)
        {
            moving = true;
            //transform.position += new Vector3(deltaPosition.x, deltaPosition.y);
            StartCoroutine(Tween(deltaPosition, 1/TilesPerSec, check));
            StartCoroutine(WaitTillNextMove(1/TilesPerSec));
        }
    }

    #endregion

    IEnumerator WaitTillNextMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }

    protected virtual IEnumerator Tween(Vector2 deltapos, float waitTime, Func<bool> check)
    {
        float time = 0;
        Vector2 from = transform.localPosition;
        Vector2 To = transform.localPosition + new Vector3(deltapos.x, deltapos.y);
        while (moving)
        {
            time += Time.deltaTime;
            float distCompleted = time / waitTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, To, distCompleted);
            yield return new WaitForEndOfFrame();
        }
    }

    protected bool CheckUp()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, ~(1 << this.gameObject.layer));

        if (hit)
        { 
            return CheckHit(hit);
        }       
        return true;
    }

    protected bool CheckDown()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, ~(1 << this.gameObject.layer));
        if (hit)
        {
            return CheckHit(hit);
        }
        return true;
    }

    protected bool CheckRight()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, ~(1 << this.gameObject.layer));

        if (hit)
        {
            return CheckHit(hit);
        }

        return true;
    }

    protected bool CheckLeft()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, ~(1 << this.gameObject.layer));
        if (hit)
        {
            return CheckHit(hit);
        }
        return true;
    }

    private bool CheckHit(RaycastHit2D hit)
    {
        if (this.GetComponent<SpriteRenderer>() != null)
            SortingLayer = this.GetComponent<SpriteRenderer>().sortingLayerName;
        if (this.GetComponentInChildren<SpriteRenderer>() != null)
            SortingLayer = this.GetComponentInChildren<SpriteRenderer>().sortingLayerName;

        if (hit.transform.GetComponent<SpriteRenderer>() != null)
        {
            if (hit.transform.GetComponent<SpriteRenderer>().sortingLayerName != SortingLayer)
                return false;
        }
        if (hit.transform.GetComponentInChildren<SpriteRenderer>() != null)
        {
            if (hit.transform.GetComponentInChildren<SpriteRenderer>().sortingLayerName != SortingLayer)
                return false;
        }
        return true;
    }

}