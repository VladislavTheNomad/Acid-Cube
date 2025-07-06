using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AcidCube
{
    public class BuildPathForLift : MonoBehaviour
    {
        [SerializeField] private List<Vector3> pathPoints;
        [SerializeField] LayerMask liftTrail; // liftTrail  
        [SerializeField] private float howMuchTimeNeedsToNextPoint;
        [SerializeField] private bool hasPower = true;

        private bool isTrailHere;
        private bool[] hasWay = new bool[4];
        Coroutine currentCoroutine;


        private Vector3 stepUp = new Vector3(0f, 0.2f, 0f);
        private Vector3 stepDown = new Vector3(0f, -0.2f, 0f);
        private Vector3 stepRight = new Vector3(0.2f, 0f, 0f);
        private Vector3 stepLeft = new Vector3(-0.2f, 0f, 0f);
        private int currentPointIndex;

        private void Awake()
        {
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
            for (int i = currentPointIndex; i < pathPoints.Count-1; i++)
            {
                yield return StartCoroutine(GoToNextPoint(pathPoints[i], pathPoints[i + 1]));
                currentPointIndex++;
            }

            for (int i = currentPointIndex; i > 0; i--)
            {
                yield return StartCoroutine(GoToNextPoint(pathPoints[i], pathPoints[i - 1]));
                currentPointIndex--;
            }

            if (hasPower) StartCoroutine(GoLift());
        }

        private IEnumerator GoToNextPoint(Vector3 currentPoint, Vector3 nextPoint)
        {
            float elapsedTime = 0f;
            float progress;
            while (elapsedTime< howMuchTimeNeedsToNextPoint)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / howMuchTimeNeedsToNextPoint;
                transform.position = Vector3.Lerp(currentPoint, nextPoint, progress);
                yield return null;
            }
        }
    }
}
