using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartInventory : MonoBehaviour
{
    public int smallParts {get; private set; }

    public UnityEvent<PartInventory> onSmallCollected;

    public void smallCollected() {
        smallParts++;
        onSmallCollected.Invoke(this);
    }

    public int mediumParts {get; private set; }

    public UnityEvent<PartInventory> onMediumCollected;

    public void mediumCollected() {
        mediumParts++;
        onMediumCollected.Invoke(this);
    }

    public int largeParts {get; private set; }

    public UnityEvent<PartInventory> onLargeCollected;

    public void largeCollected() {
        largeParts++;
        onLargeCollected.Invoke(this);
    }
    
}
