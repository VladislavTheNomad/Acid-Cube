using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class MovingWall : MonoBehaviour
    {

        // internal
        private bool startOpening = true;
        private bool startClosing = false;

        private float startPositionY;
        private float endPositionY;
        private float currentY;

        //settings
        [SerializeField] private float timeForWallMoving = 2f;
        [SerializeField] private float movePositionY;
        [SerializeField] private bool hasPower = true;


        private void Awake()
        {
            startPositionY = transform.localPosition.y;
            endPositionY = startPositionY + movePositionY;
        }

        private void FixedUpdate()
        {
            if (startOpening && hasPower)
            {
                startOpening = false;
                StartCoroutine(WallMoving(timeForWallMoving));
            }
            else if (startClosing && hasPower)
            {
                startClosing = false;
                StartCoroutine(WallClosing(timeForWallMoving));
            }
        }

        public void InstatnClosing()
        {
            hasPower = false;
            StopAllCoroutines();
            StartCoroutine(WallMoving(0.05f));
        }

        private IEnumerator WallMoving(float time)
        {
            float elapsedTimeForWall = 0f;
            float nowPositionY = transform.localPosition.y; // get local Y 
            while (elapsedTimeForWall < time)
            {
                elapsedTimeForWall += Time.deltaTime;
                currentY = Mathf.Lerp(nowPositionY, endPositionY, elapsedTimeForWall / time);
                transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
                yield return null;
            }
            startClosing = true;
        }

        IEnumerator WallClosing(float time)
        {
            float elapsedTimeForSpike = 0f;
            float nowPositionY = transform.localPosition.y; // get local Y 
            while (elapsedTimeForSpike < time)
            {
                elapsedTimeForSpike += Time.deltaTime;
                currentY = Mathf.Lerp(nowPositionY, startPositionY, elapsedTimeForSpike / time);
                transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
                yield return null;
            }
            startOpening = true;
        }
    }
}
