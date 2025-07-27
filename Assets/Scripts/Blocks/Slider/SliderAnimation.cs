using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class SliderAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject movingSlideBlock;

        [SerializeField] private float movingSpeedOpening;
        [SerializeField] private float movingSpeedClosing;
        [SerializeField] private float waitingTimeWhenClose;
        [SerializeField] private float waitingTimeWhenOpen;
        [SerializeField] private float delayTime;
        [SerializeField] private bool hasPower = true;

        [SerializeField] public bool isOpening;
        [SerializeField] public bool isClosing = true;
        [SerializeField] public bool isWaiting;


        private float startZposition;
        private float finalZposition;
        private float currentZposition;


        private void Start()
        {
            startZposition = movingSlideBlock.transform.position.z;
            finalZposition = startZposition - 1.1f;
            if(hasPower) StartCoroutine(WaitingTime(delayTime));
        }

        public void TurnOn()
        {
            hasPower = true;
            StartCoroutine(WaitingTime(delayTime));
        }

        private void Update()
        {
            if (isWaiting || !hasPower) return;

            if(isOpening)
            {             
                ChangeSliderPosition(1, movingSpeedOpening);

                if (currentZposition >= startZposition)
                {
                    ModeChanger();
                }
            }

            else if (isClosing)
            {
                ChangeSliderPosition(-1, movingSpeedClosing);

                if (currentZposition <= finalZposition)
                {
                    ModeChanger();
                }
            }
        }

        private void ChangeSliderPosition(float modificator, float speed)
        {
            movingSlideBlock.transform.Translate(modificator * Vector3.forward * (speed * Time.deltaTime));
            currentZposition = movingSlideBlock.transform.position.z;
        }

        private void ModeChanger()
        {
            if(isClosing)
            {
                StartCoroutine(WaitingTime(waitingTimeWhenClose));
                isClosing = false;
                isOpening = true;
            }
            else if (!isClosing)
            {
                StartCoroutine(WaitingTime(waitingTimeWhenOpen));
                isClosing = true;
                isOpening = false;
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

    
    
