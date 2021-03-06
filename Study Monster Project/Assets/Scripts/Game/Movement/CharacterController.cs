﻿using UnityEngine;
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

    public bool moving;

    protected bool success = false;
    protected Vector2 from; // Original position from where it's moving
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

    public void move(Vector2 deltaPosition, float TilesPerSec, bool ColCheck = true)
    {
        if (!moving)
        {
            moving = true;
            //transform.position += new Vector3(deltaPosition.x, deltaPosition.y);
            StartCoroutine(Tween(deltaPosition, 1/TilesPerSec, ColCheck));
            StartCoroutine(WaitTillNextMove(1/TilesPerSec));
        }
    }

    #endregion

    IEnumerator WaitTillNextMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }

    protected virtual IEnumerator Tween(Vector2 deltapos, float waitTime, bool ColCheck = true)
    {
        float time = 0;
        from = transform.localPosition;
        Vector2 To = transform.localPosition + new Vector3(deltapos.x, deltapos.y);
        while (moving)
        {
            time += Time.deltaTime;
            float distCompleted = time / waitTime;
            if (ColCheck) // If you are supposed to check for collisions while moving, do it.
            {
                success = CompleteMoveCheck(deltapos, from, distCompleted);
                if (!success)
                    break;
            }
            transform.localPosition = Vector2.Lerp(from, To, distCompleted);
            yield return new WaitForEndOfFrame();
        }
    }

    private bool CompleteMoveCheck(Vector2 To, Vector2 BackTo, float distCompleted)
    {
        if (!Raycast(To, (1f-distCompleted)))
        {
            CancelMove(BackTo);
            return false;
        }
        return true;
    }

    protected void CancelMove(Vector2 BackTo) // Cancel the current move and go back.
    {
        moving = false;
        transform.localPosition = BackTo;
    }

    protected bool Raycast(Vector2 Direction, float Distance)
    {
        var hit = Physics2D.Raycast(transform.position, Direction, Distance, ~(1 << this.gameObject.layer));

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
        else if (hit.transform.GetComponentInChildren<SpriteRenderer>() != null)
        {
            if (hit.transform.GetComponentInChildren<SpriteRenderer>().sortingLayerName != SortingLayer)
                return false;
        }
        else
            return false;
        return true;
    }

    /// <summary>
    /// Turn Player to Said Direction:
    ///
    /// </summary>
    /// <param name="direction"> 0 -> Down.
    /// 1 -> Up.
    /// 2 -> Left.
    /// 3 -> Right.</param>
    public void Turn(int direction, GameObject SpriteObject, Animator anim)
    {
        if(direction == 2)
        {
            if (SpriteObject.transform.localScale.x > 0f)
                SpriteObject.transform.localScale = new Vector3(-SpriteObject.transform.localScale.x, SpriteObject.transform.localScale.y, SpriteObject.transform.localScale.z);
            anim.SetInteger("Direction", 2);
        }
        else if(direction == 3)
        {
            if (SpriteObject.transform.localScale.x < 0f)
                SpriteObject.transform.localScale = new Vector3(-SpriteObject.transform.localScale.x, SpriteObject.transform.localScale.y, SpriteObject.transform.localScale.z);
            anim.SetInteger("Direction", 2);
        }
        else
        {
            anim.SetInteger("Direction", direction);
        }
    }

}