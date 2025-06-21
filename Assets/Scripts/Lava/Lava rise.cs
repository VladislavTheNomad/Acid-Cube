using System.Collections;
using UnityEngine;

public class Lava—ontroller : MonoBehaviour
{
    [SerializeField] float startPositionY;
    [SerializeField] float endPositionY;
    [SerializeField] float timeForReachingEndPoint;

    private float currentlyY;

    void Start()
    {
        startPositionY = transform.position.y; //start point of Lava
        //endPositionY = startPositionY + 100f; // At the moment the endpoint is implemented using input in Insector
        StartCoroutine(LavaIsMoving());
    }

    IEnumerator LavaIsMoving()
    {
        float elapsedTime = 0f; // detect progress over time
        while (elapsedTime < timeForReachingEndPoint)
        {
            elapsedTime += Time.deltaTime;
            currentlyY = Mathf.Lerp(startPositionY, endPositionY, elapsedTime/timeForReachingEndPoint); // get linear progress along Y axis of the Lava from start to finish 
            transform.position = new Vector3(transform.position.x, currentlyY, transform.position.z);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOverMenu.instance.OpenGameOverMenu();
        }
    }
}
