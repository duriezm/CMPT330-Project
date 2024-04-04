using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeToMainGame : Interactable
{
    [SerializeField] private GameObject cube;

    public TextMeshProUGUI collectableText;
    public float collectable;

    void Start()
    {
        collectable = 1;
    }

    protected override void Interact()
    {
        collectableText.SetText(collectable.ToString());
        Destroy(gameObject);
        SceneManager.LoadScene("GameMain");
    }
}