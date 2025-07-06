using AcidCube;
using UnityEngine;

namespace AcidCube
{
    public class LiftGetPowe : MonoBehaviour
    {
        [SerializeField] private BuildPathForLift lift;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            lift.GetPower();   
        }
    }
}
