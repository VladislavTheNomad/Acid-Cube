using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;


    // PAUSE MENU INTERFACE
    public void OpenOrClosePauseMenu()
    {
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            OpenPauseMenu();
        }
    }
    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.05f;
    }

    // BUTTON "RETRY"

    public void RetryTheGame()
    {
        Time.timeScale = 1f;
        Scene currentLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentLevel.name);
    }

    // BUTTON "QUIT"

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
