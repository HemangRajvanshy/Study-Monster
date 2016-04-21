using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable: MonoBehaviour, IInteractable {

    public void Interact()
    {
        Debug.Log("Interacting With: " + gameObject.name);
    }
}


public interface IInteractable
{
    void Interact();
}