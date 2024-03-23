using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add me!!

public class OptionsButton : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
