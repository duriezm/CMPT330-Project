using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverUI;

    public void gameOver()
    {
        Cursor.visible = true;
        gameOverUI.SetActive(true);
    }
    public void MainMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void ResetGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Tutorial");
    }
}
