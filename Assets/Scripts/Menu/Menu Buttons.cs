using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void OnPlayButton()
    {
        StartCoroutine(OnPlayButtonWait());
        
    }
    public void OnOptionsButton()
    {
        StartCoroutine(OptionsScreenWait());
    }
    public void OnQuitButton()
    {
        StartCoroutine(OnQuitButtonWait());
        
    }
    public void OnTutorialButton()
    {
        StartCoroutine(OnTutorialButtonWait());
        
    }
    IEnumerator OnQuitButtonWait()
    {
        yield return new WaitForSeconds(.5f);
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    IEnumerator OptionsScreenWait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("OptionsScreen");
    }
    IEnumerator OnTutorialButtonWait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Tutorial");
    }
    IEnumerator OnPlayButtonWait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Main");
    }
}
