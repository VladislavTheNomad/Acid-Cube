using UnityEngine;

namespace AcidCube
{
    public class DoorScriptRed : MonoBehaviour
    {
        private KeyCounterAndDisplayer gameManager;

        private void Awake()
        {
            gameManager = FindAnyObjectByType<KeyCounterAndDisplayer>();
            if (gameManager == null)
            {
                Debug.Log("NO KeyCounterAndDisplayer component or GameManager GameObject");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() && gameManager.keys.Contains("red"))
            {
                Destroy(gameObject);
            }
        }

    }

}