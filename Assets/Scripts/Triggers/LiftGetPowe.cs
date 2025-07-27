using UnityEngine;

namespace AcidCube
{
    public class LiftGetPowe : MonoBehaviour
    {
        [SerializeField] private LiftMoving lift;

        private void OnTriggerEnter(Collider other)
        {
            if (lift.hasPower || !other.GetComponent<PlayerController>()) return;

            lift.GetPower();   
        }
    }
}
