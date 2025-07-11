using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AcidCube
{
    public class BuildPathForLift : MonoBehaviour
    {
        [SerializeField] public List<Vector3> pathPoints; //use also in ObseringIfPlayerAlive script
        [SerializeField] LayerMask liftTrail;
        [SerializeField] private float howMuchTimeNeedsToNextPoint;
        [SerializeField] public bool hasPower = true; //use also in ObseringIfPlayerAlive script

        public bool originalPowerMode { get; private set; } //use also in ObseringIfPlayerAlive script
        private bool isTrailHere;
        private bool[] hasWay = new bool[4];
        public Coroutine currentCoroutine { get; set; } //use also in ObseringIfPlayerAlive script


        private Vector3 stepUp = new Vector3(0f, 0.2f, 0f);
        private Vector3 stepDown = new Vector3(0f, -0.2f, 0f);
        private Vector3 stepRight = new Vector3(0.2f, 0f, 0f);
        private Vector3 stepLeft = new Vector3(-0.2f, 0f, 0f);
        public int currentPointIndex; //use also in ObseringIfPlayerAlive script

        public Rigidbody rb { get; private set; } //use also in ObseringIfPlayerAlive script

        private void Awake()
        {
            originalPowerMode = hasPower;
            rb = GetComponent<Rigidbody>();
            ConstructPath();
        }

        [ContextMenu("Construct Path")]
        private void ConstructPath()
        {
            pathPoints = new List<Vector3>();
            pathPoints.Add(transform.position);

            do
            {
                hasWay[0] = CheckPath(stepUp);

                if (hasWay[0] == true) continue;

                hasWay[1] = CheckPath(stepDown);

                if (hasWay[1] == true) continue;

                hasWay[2] = CheckPath(stepRight);

                if (hasWay[2] == true) continue;

                hasWay[3] = CheckPath(stepLeft);
            }
            while (hasWay.Contains(true));

            if (hasPower) currentCoroutine = StartCoroutine(GoLift());
        }

        private bool CheckPath(Vector3 path)
        {
            isTrailHere = Physics.Raycast(pathPoints.Last() + path, -Vector3.forward, 1f, liftTrail);
            Debug.DrawRay(pathPoints.Last() + path, -Vector3.forward, Color.red, 100f);
            if (isTrailHere && !pathPoints.Contains(pathPoints.Last() + path))
            {
                pathPoints.Add(pathPoints.Last() + path);
                return true;
            }
            return false;
        }
        [ContextMenu("Go Lift")]
        private IEnumerator GoLift()
        {
            for (int i = currentPointIndex; i < pathPoints.Count - 1; i++)
                {
                    yield return StartCoroutine(GoToNextPoint(pathPoints[i], pathPoints[i + 1]));
                    currentPointIndex++;
                }
            
            for (int i = currentPointIndex; i > 0; i--)
                {
                    yield return StartCoroutine(GoToNextPoint(pathPoints[i], pathPoints[i - 1]));
                    currentPointIndex--;
                }
            
            if (hasPower) currentCoroutine = StartCoroutine(GoLift());
        }

        private IEnumerator GoToNextPoint(Vector3 currentPoint, Vector3 nextPoint)
        {
            float elapsedTime = 0f;
            float progress;
            while (elapsedTime< howMuchTimeNeedsToNextPoint)
            {
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
