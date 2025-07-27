using UnityEngine;

namespace AcidCube
{
    public class ShardsDestruction : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.GetComponent<PlayerController>())
            {
                Destroy(gameObject);
            }
        }
    }
}
