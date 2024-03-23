using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    [SerializeField]
    public string promptMessage;

    // this function will be called from our player
    public void BaseInteract()
    {
        if (useEvents)        
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        
        Interact();
    }
    protected virtual void Interact()
    {

    }
}
