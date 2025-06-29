using UnityEditor.Build.Content;
using UnityEngine;

public class GrabRedKey : MonoBehaviour
{
    [SerializeField] private KeyCounterAndDisplayer gameManager;
    private string color = "red";

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<KeyCounterAndDisplayer>();
        if ( gameManager == null )
        {
            Debug.Log("NO KeyCounterAndDisplayer component or GameManager GameObject");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.TakeAKey(color);
            Destroy(gameObject);
        }
    }
}
