using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class MovingOfBladeTrap : MonoBehaviour
    {
        [SerializeField] private BladeTrapAnimations fromAnimationScript;
        [SerializeField] private BladeTrapPathConstructor fromConstructorScript;
        [SerializeField] private BladeTrapAudio fromAudioScript;

        [Header("On which direction will the enemy go?")]
        [SerializeField] private Direction directionPath;

        [SerializeField] private float bladeTrapSpeed;
        [SerializeField] private float waitingTime;

        private Vector3 currentTarget;
        public bool isWaiting { get; private set; }
        public Direction currentDirection { get; private set; }

        private void Start()
        {
            currentDirection = directionPath;
            fromAnimationScript.AnimationChanger(directionPath);
            isWaiting = false;

            if (directionPath == Direction.Right) currentTarget = fromConstructorScript.farRightPoint;
            else currentTarget = fromConstructorScript.farLeftPoint;
        }

        private void DirectionChanger(Direction newDirection)
        {
            currentDirection = newDirection;
            fromAnimationScript.AnimationChanger(currentDirection);
        }

        private void Update()
        {
            if(Vector3.Distance(transform.position, currentTarget) < 0.01f && !isWaiting)
            {
                isWaiting = true;
                fromAudioScript.TurnOffSound();
                StartCoroutine(WaitSomeTime());
                if(currentDirection == Direction.Right)
                {
                    DirectionChanger(Direction.Left);
                    currentTarget = fromConstructorScript.farLeftPoint;
                }
                else if (currentDirection == Direction.Left)
                {
                    DirectionChanger(Direction.Right);
                    currentTarget = fromConstructorScript.farRightPoint;
                }
            }

            if (!isWaiting)
            {
                Vector3 directionToTarget = (currentTarget - transform.position).normalized;
                transform.Translate(directionToTarget * (bladeTrapSpeed * Time.deltaTime), Space.World);
            }
        }

        private IEnumerator WaitSomeTime()
        {
            yield return new WaitForSeconds(waitingTime);
            isWaiting = false;
            fromAudioScript.TurnOnSound();
        }
    }
}
