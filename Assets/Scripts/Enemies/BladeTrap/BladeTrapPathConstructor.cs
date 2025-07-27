using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AcidCube

{
    public class BladeTrapPathConstructor : MonoBehaviour
    {
        [SerializeField] private List<Vector3> pathPoints;
        [SerializeField] private LayerMask groundLayer;

        [Header("Leave this to False/False if this is standard BladeTrap on the floor")]
        [SerializeField] public bool isLeftSideTrap;
        [SerializeField] public bool isRightSideTrap;

        private bool isGroundHere;
        private bool isGroundWallHere;

        public Vector3 farRightPoint { get; private set; }
        public Vector3 farLeftPoint { get; private set; }

        private void Awake()
        {
            ConstructPath();
        }

        [ContextMenu("Construct Path")]
        private void ConstructPath()
        {
            pathPoints = new List<Vector3>();
            pathPoints.Add(transform.position);

            farRightPoint = transform.position;
            farLeftPoint = transform.position;

            if (isLeftSideTrap && !isRightSideTrap)
            {
                GroundConstructor(Vector3.up, Vector3.down, Vector3.right);
            }
            else if (isRightSideTrap && !isLeftSideTrap)
            {
                GroundConstructor(Vector3.down, Vector3.up, Vector3.left);
            }
            else if (!isLeftSideTrap && !isRightSideTrap) { GroundConstructor(Vector3.left, Vector3.right, Vector3.down); }
        }

        private void GroundConstructor(Vector3 dir1, Vector3 dir2, Vector3 whichIsDown)
        {
            while (true)
            {
                if (!CheckPath(dir1 * 0.2f, whichIsDown))
                {
                    continue;
                }
                farRightPoint = pathPoints.Last();
                break;
            }
            while (true)
            {
                if (!CheckPath(dir2 * 0.2f, whichIsDown))
                {
                    continue;
                }
                farLeftPoint = pathPoints.Last();
                break;
            }
        }

        private bool CheckPath(Vector3 path, Vector3 toGround)
        {
            isGroundHere = Physics.Raycast(pathPoints.Last() + path, toGround, 0.2f, groundLayer);
            isGroundWallHere = Physics.Raycast(pathPoints.Last(), path, 0.2f, groundLayer);

            Debug.DrawRay(pathPoints.Last() + path, toGround, Color.red, 100f);
            Debug.DrawRay(pathPoints.Last(), path, Color.red, 100f);

            if (isGroundHere && !isGroundWallHere)
            {
                pathPoints.Add(pathPoints.Last() + path);
                return false;
            }
            return true;
        }
    }
}
