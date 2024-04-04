using UnityEngine;

public class LevitatingCube : Interactable
{
    [SerializeField] private GameObject cube;

    private bool spinCube;

    protected override void Interact()
    {
        spinCube = !spinCube;
        cube.GetComponent<Animator>().SetBool("isSpinning", spinCube);
    }
}
