using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics.Geometry;
using UnityEngine;

namespace AcidCube
{
    public class BuildPathForLift : MonoBehaviour
    {
        [SerializeField] private LiftMoving liftMovingScript;

        [SerializeField] public List<Vector3> pathPoints; //use also in ObseringIfPlayerAlive script
        [SerializeField] LayerMask liftTrail;
       
        private bool[] hasWay = new bool[4];
        public int currentPointIndex; //use also in ObseringIfPlayerAlive script
        private Direction[] currentDir = {Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        private Vector3 currentVector;

        private void Awake()
        {
            ConstructPath();
        }

        [ContextMenu("Construct Path")]
        private void ConstructPath()
        {
            pathPoints = new List<Vector3>();
            pathPoints.Add(RoundVector(transform.position));

            do
            {
                for (int i = 0; i < hasWay.Count(); i++)
                {
                    currentVector = DirectionUtils.ToVector(currentDir[i]);
                    hasWay[i] = CheckPath(currentVector * 0.2f);
                }
            }
            while (hasWay.Contains(true));

            if (liftMovingScript.hasPower) liftMovingScript.currentCoroutine = StartCoroutine(liftMovingScript.GoLift());
        }

        private bool CheckPath(Vector3 path)
        {
            Vector3 roundedPath = RoundVector(pathPoints.Last() + path);

            bool isTrailHere = Physics.Raycast(roundedPath, -Vector3.forward, 1f, liftTrail);

            Debug.DrawRay(pathPoints.Last() + path, -Vector3.forward, Color.red, 100f);

            if (isTrailHere && !pathPoints.Contains(roundedPath))
            {
                pathPoints.Add(roundedPath);
                return true;
            }
            return false;
        }

        private Vector3 RoundVector(Vector3 newVector)
        {
            return new Vector3(
                Mathf.Round(newVector.x * 10f) / 10f,
                Mathf.Round(newVector.y * 10f) / 10f,
                Mathf.Round(newVector.z * 10f) / 10f
                );
        }
    }
}
