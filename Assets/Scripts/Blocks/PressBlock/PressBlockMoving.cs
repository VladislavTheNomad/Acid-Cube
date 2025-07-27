using System.Collections;
using System.Timers;
using UnityEngine;

namespace AcidCube
{
    public class PressBlockMoving : MonoBehaviour
    {
        [SerializeField] private GameObject movingPressBlock;
        [SerializeField] private GameObject deathBlock;

        [SerializeField] private float movingSpeedOpening;
        [SerializeField] private float movingSpeedClosing;
        [SerializeField] private float waitingTime;
        [SerializeField] private float delayTime;
        [SerializeField] private float movingRange;

        private bool isOpening = true;
        private bool isClosing;
        private bool isWaiting;

        private float startYposition;
        private float finalYposition;
        private float currentYposition;


        private void Start()
        {
            startYposition = movingPressBlock.transform.position.y;
            finalYposition = startYposition - movingRange;
            StartCoroutine(WaitingTime(delayTime));
        }

        private void Update()
        {
            if (isWaiting) return;

            if (isOpening)
            {
                ChangePressPosition(-1, movingSpeedOpening);

                if (currentYposition - 0.5f <= finalYposition)
                {
                    deathBlock.SetActive(true);
                }

                if (currentYposition <= finalYposition)
                {

                    ModeChanger();
                }
            }

            else if (isClosing)
            {

                ChangePressPosition(1, movingSpeedClosing);

                if (currentYposition >= startYposition)
                {
                    ModeChanger();
                }
            }
        }

        private void ChangePressPosition(float modificator, float speed)
        {
            movingPressBlock.transform.Translate(modificator * Vector3.up * (speed * Time.deltaTime));
            currentYposition = movingPressBlock.transform.position.y;
        }

        private void ModeChanger()
        {
            StartCoroutine(WaitingTime(waitingTime));
            if (isClosing)
            {
                isClosing = false;
                isOpening = true;
            }
            else if (!isClosing)
            {
                isClosing = true;
                isOpening = false;
                deathBlock.SetActive(false);
            }
        }

        private IEnumerator WaitingTime(float waitingTime)
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitingTime);
            isWaiting = false;
        }
    }
}
