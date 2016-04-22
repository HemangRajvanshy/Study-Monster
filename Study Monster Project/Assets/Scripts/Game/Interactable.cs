using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable: MonoBehaviour, IInteractable {

    public List<string> Dialogue = new List<string>();

    public List<string> Interact()
    {
        return Dialogue;
    }
}


public interface IInteractable
{
    List<string> Interact();
}