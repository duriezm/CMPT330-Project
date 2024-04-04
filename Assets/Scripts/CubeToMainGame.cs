using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeToMainGame : Interactable
{
    [SerializeField] private GameObject cube; 
    
    protected override void Interact()
    {
        SceneManager.LoadScene("GameMain");
    }
}