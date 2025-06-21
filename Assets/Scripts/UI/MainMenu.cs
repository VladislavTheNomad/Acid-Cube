using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //if in PlayMode
#else
        Application.Quit(); //if the game is running in the application
#endif
    }
}
