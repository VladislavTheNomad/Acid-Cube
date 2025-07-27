using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class LavaDropAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject movingLavaDrop;

        [Header("Around 10")]
        [SerializeField] private float startSpeedToUp;
        [SerializeField] private float waitingOnDownTime;
        [SerializeField] private float delayTime;
        [SerializeField] private float movingRangeY;

        // Deformation (scale changing)

        [SerializeField] private Vector3 scaleOnTop = new Vector3(0.5f, 0.3f, 0.4f);
        [SerializeField] private Vector3 scaleOnBottom = new Vector3(0.2f, 0.7f, 0.4f);

        // own

        private bool isToUp;
        private bool isToDown = true;
        private bool isWaiting;

        private float startYposition;
        private float finalYposition;
        private float currentYposition;

        private const float gravitation = 9.8f;
        private float currentSpeed = 0f;

        private void Start()
        {
            movingLavaDrop.transform.localScale = scaleOnTop;

            startYposition = movingLavaDrop.transform.position.y;
            finalYposition = startYposition - movingRangeY;
            
            StartCoroutine(WaitingTime(delayTime));
        }

        private void Update()
        {
            if (isWaiting) return;

            DropDeformation();

            if (isToDown)
            {
                currentSpeed += gravitation * Time.deltaTime;
                ChangeLavaDropPosition(-1, currentSpeed);
                currentYposition = movingLavaDrop.transform.position.y;

                if (currentYposition <= finalYposition)
                {
                    currentSpeed = startSpeedToUp;
                    ModeChanger();
                }
            }

            if (isToUp)
            {
                currentSpeed -= gravitation * Time.deltaTime;

                if (currentSpeed > 0f)
                {
                    ChangeLavaDropPosition(1, currentSpeed);
                }
                else
                {
                    ModeChanger();
                }
            }
        }

        private void DropDeformation()
        {
            float t = (movingLavaDrop.transform.position.y - finalYposition) / (startYposition - finalYposition);
            t = Mathf.Clamp01(t);
            Vector3 newScale = Vector3.Lerp(scaleOnBottom, scaleOnTop, t);
            movingLavaDrop.transform.localScale = newScale;
        }

        private void ChangeLavaDropPosition(float modificator, float speed)
        {
            movingLavaDrop.transform.Translate(modificator * Vector3.up * (speed * Time.deltaTime));
            currentYposition = movingLavaDrop.transform.position.y;
        }

        private void ModeChanger()
        {
            if (isToDown)
            {
                StartCoroutine(WaitingTime(waitingOnDownTime));
                isToDown = false;
                isToUp = true;
            }
            else if (!isToDown)
            {
                isToDown = true;
                isToUp = false;
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