
using System.Collections;
using UnityEngine;

namespace AcidCube
{

    public class Lava—ontroller : MonoBehaviour
    {

        [SerializeField] private float endPositionY;
        [SerializeField] private float timeForReachingEndPoint;

        private float currentlyY;
        private float startPositionY;

        private void Start()
        {
            startPositionY = transform.position.y; //start point of Lava
            StartCoroutine(LavaIsMoving());
        }

        private IEnumerator LavaIsMoving()
        {
            float elapsedTime = 0f; // detect progress over time
            while (elapsedTime < timeForReachingEndPoint)
            {
                elapsedTime += Time.deltaTime;
                currentlyY = Mathf.Lerp(startPositionY, endPositionY, elapsedTime / timeForReachingEndPoint); // get linear progress along Y axis of the Lava from start to finish 
                transform.position = new Vector3(transform.position.x, currentlyY, transform.position.z);
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;
            GameOverMenu.instance.OpenGameOverMenu();
        }
    }
}
