using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnOptionsButton()
    {
        SceneManager.LoadScene("OptionsScreen");
    }
    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void OnTutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
