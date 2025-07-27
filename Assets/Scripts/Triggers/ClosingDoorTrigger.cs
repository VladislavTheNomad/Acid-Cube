using UnityEngine;

namespace AcidCube
{
    public class ClosingDoorTrigger : MonoBehaviour
    {
        [SerializeField] private MovingWall movingDoor;

        private bool isFirstTime = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            if (isFirstTime)
            {
                isFirstTime = false;
                movingDoor.InstatnClosing();
            }
        }
    }
}
