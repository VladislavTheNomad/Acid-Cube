
using UnityEngine;

namespace AcidCube
{
    public class Spike : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;
            GameOverMenu.instance.OpenGameOverMenu();
        }
    }
}