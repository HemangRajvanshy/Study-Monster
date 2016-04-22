using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class WorldObject : MonoBehaviour {

    public bool CanOverlap = true; // Variable that tells whether the player can be hidden by the object in case he approaches from the top.

    private SpriteRenderer Renderer;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

	public void OnTriggerStay2D(Collider2D col)
    {
        if (CanOverlap && col.transform.tag == "Player")
        {
            if (transform.position.y < col.transform.position.y)
                Renderer.sortingLayerName = "WorldObject+";
            else
                Renderer.sortingLayerName = "WorldObject";
        }
    }
}
