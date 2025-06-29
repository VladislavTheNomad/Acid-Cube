using UnityEngine;

public class DestoyingShards : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player got crystal shard");
            }
            Destroy(gameObject);
        }


    }
}
