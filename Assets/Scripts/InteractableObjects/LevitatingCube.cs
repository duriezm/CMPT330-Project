using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevitatingCube : Interactable
{
    [SerializeField] private GameObject cube;

    private bool spinCube;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        spinCube =! spinCube;
        cube.GetComponent<Animator>().SetBool("isSpinning", spinCube);
    }
}
