using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
    public void OnBackButton()
    {
        StartCoroutine(OnBackButtonWait());
    }
    IEnumerator OnBackButtonWait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Main Menu");
    }
}
