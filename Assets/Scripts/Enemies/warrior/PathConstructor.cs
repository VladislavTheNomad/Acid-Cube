using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AcidCube

{
    public class PathConstructor : MonoBehaviour
    {
        [SerializeField] private List<Vector3> pathPoints;
        [SerializeField] private float howMuchTimeNeedsToNextPoint;
        [SerializeField] private LayerMask groundLayer; 
        [SerializeField] private float enemySpeed;

        [Header("On which direction will the enemy go?")]
        [SerializeField] private bool goToRight = true;
        [SerializeField] private bool goToLeft = false;

        private Vector3 stepLeft = new Vector3(0.5f, 0f, 0f);
        private Vector3 stepRight = new Vector3(-0.5f, 0f, 0f);
        private bool isGroundHere;
        private bool isGroundWallHere;

        private Vector3 farRightPoint;
        private Vector3 farLeftPoint;

        private Vector3 direction;



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

            while (true)
            {
                if (CheckPath(stepLeft))
                {
                    farRightPoint = pathPoints.Last();
                    break;
                }
            }
            while (true)
            {
                if (CheckPath(stepRight))
                {
                    farLeftPoint = pathPoints.Last();
                    break;
                }
            }
        }

        private bool CheckPath(Vector3 path)
        {
            isGroundHere = Physics.Raycast(pathPoints.Last() + path, Vector3.down, 1f, groundLayer);
            isGroundWallHere = Physics.Raycast(pathPoints.Last(), path, 1f, groundLayer);

            Debug.DrawRay(pathPoints.Last() + path, Vector3.down, Color.red, 100f);
            Debug.DrawRay(pathPoints.Last(), path, Color.red, 100f);

            if (isGroundHere && !isGroundWallHere)
            {
                pathPoints.Add(pathPoints.Last() + path);
                return false;
            }
            return true;
        }

        private void Update()
        {
            if (goToRight)
            {
                if (farRightPoint.x > transform.position.x)
                {
                    direction = (farRightPoint - transform.position).normalized;

                    transform.Translate(direction * (enemySpeed * Time.deltaTime));
                }
                else
                {
                    goToRight = false;
                    goToLeft = true;
                }
            }
            else if (goToLeft)
            {
                if (farLeftPoint.x < transform.position.x)
                {
                    direction = (farLeftPoint - transform.position).normalized;

                    transform.Translate(direction * (enemySpeed * Time.deltaTime));
                }
                else
                {
                    goToLeft = false;
                    goToRight = true;
                }
            }
        }
    }
}
