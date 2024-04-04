using UnityEngine;
using TMPro;

public class LevitatingCube : Interactable
{
    [SerializeField] private GameObject cube;

    private bool spinCube;

    [SerializeField]
    private bool interactSwitch;

    public TextMeshProUGUI collectableText;
    public float collectable;
    void Start()
    {
        collectable = 1;
    }
    protected override void Interact()
    {
        if (interactSwitch == true)
        {
            collectableText.SetText(collectable.ToString());
            Destroy(gameObject);
        }
        else
        {
            spinCube = !spinCube;
            cube.GetComponent<Animator>().SetBool("isSpinning", spinCube);
        }
    }
}
