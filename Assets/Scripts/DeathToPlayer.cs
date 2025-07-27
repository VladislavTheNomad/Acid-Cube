using UnityEngine;


namespace AcidCube
{
    public class DeathToPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            GameOverMenu.instance.OpenGameOverMenu();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.GetComponent<PlayerController>()) return;

            GameOverMenu.instance.OpenGameOverMenu();
        }
    }
}
