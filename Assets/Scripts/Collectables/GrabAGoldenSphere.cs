using UnityEngine;

namespace AcidCube
{
    public class GrabAGoldenSphere : MonoBehaviour
    {
        [SerializeField] private Material newPlayerMaterial;

        private KeyCounterAndDisplayer gameManager;
        private PlayerController playerController;

        private void Awake()
        {
            gameManager = FindAnyObjectByType<KeyCounterAndDisplayer>();
            if (gameManager == null)
            {
                Debug.Log("NO KeyCounterAndDisplayer component or GameManager GameObject");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            playerController = GameObject.Find(other.name).GetComponent<PlayerController>();
            playerController.ChangeToGolden(newPlayerMaterial);
            Destroy(gameObject);
        }
    }
}
