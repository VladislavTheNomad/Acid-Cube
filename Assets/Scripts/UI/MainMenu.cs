using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AcidCube
{

    public class MainMenu : MonoBehaviour
    {
        public void StartNewGame()
        {
            SceneManager.LoadScene("Level1");
        }

        public void Exit()
        {
            Application.Quit(); //if the game is running in the application
        }
    }
}
