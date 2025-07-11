using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AcidCube 
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverMenu;
        [SerializeField] private SavingProgress savingProgress;
        [SerializeField] private Lava—ontroller lavaBlock;

        private PlayerController player;

        public static GameOverMenu instance { get; private set; }

        public static event Action OnGameOverTriggered;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
            player = FindAnyObjectByType<PlayerController>();
        }


        // MENU INTERFACE

        public void OpenGameOverMenu()
        {
            OnGameOverTriggered?.Invoke();

            if (savingProgress.pointOfRestart.transform.position.y > lavaBlock.transform.position.y + 6f)
            {
                player.transform.position = savingProgress.pointOfRestart.transform.position;
            }
            else 
            {
                _gameOverMenu.SetActive(true);
                Time.timeScale = 0f;
            }
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
}
