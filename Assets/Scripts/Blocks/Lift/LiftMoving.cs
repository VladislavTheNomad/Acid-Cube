using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class LiftMoving : MonoBehaviour
    {
        // Other Scripts
        [SerializeField] private BuildPathForLift liftScript;

        // Settings
        [SerializeField] public bool hasPower = true; //use also in ObseringIfPlayerAlive script
        [SerializeField] private float howMuchTimeNeedsToNextPoint;

        // own
        public bool originalPowerMode { get; private set; } //use also in ObseringIfPlayerAlive script

        public Rigidbody rb { get; private set; } //use also in ObseringIfPlayerAlive script
        public Coroutine currentCoroutine { get; set; } //use also in ObseringIfPlayerAlive script

        private void Awake()
        {
            originalPowerMode = hasPower;
            rb = GetComponent<Rigidbody>();
        }

        [ContextMenu("Go Lift")]
        public IEnumerator GoLift()
        {
            for (int i = liftScript.currentPointIndex; i < liftScript.pathPoints.Count - 1; i++)
            {
                if (!hasPower) break;
                yield return StartCoroutine(GoToNextPoint(liftScript.pathPoints[i], liftScript.pathPoints[i + 1]));
                liftScript.currentPointIndex++;
            }

            for (int i = liftScript.currentPointIndex; i > 0; i--)
            {
                if (!hasPower) break;
                yield return StartCoroutine(GoToNextPoint(liftScript.pathPoints[i], liftScript.pathPoints[i - 1]));
                liftScript.currentPointIndex--;
            }

            if (hasPower) currentCoroutine = StartCoroutine(GoLift());
        }

        private IEnumerator GoToNextPoint(Vector3 currentPoint, Vector3 nextPoint)
        {
            float elapsedTime = 0f;
            float progress;
            while (elapsedTime < howMuchTimeNeedsToNextPoint)
            {
                if (!hasPower) break;
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / howMuchTimeNeedsToNextPoint;
                rb.MovePosition(Vector3.Lerp(currentPoint, nextPoint, progress));
                yield return null;
            }
        }

        public void StopMovement()
        {
            hasPower = false;
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
        }

        public void GetPower()
        {
            hasPower = true;
            currentCoroutine = StartCoroutine(GoLift());
        }
    }
}
