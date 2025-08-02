using UnityEngine;

namespace AcidCube
{
    public class ResetStartPoint : MonoBehaviour, IResetStartPoint
    {
        private Vector3 startPosition;
        private Quaternion startRotation;

        private void OnEnable()
        {
            GameOverMenu.OnGameOverTriggered += BackToStartPosition;
        }

        private void Awake()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        public void BackToStartPosition()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
