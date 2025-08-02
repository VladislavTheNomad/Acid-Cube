using UnityEngine;


namespace AcidCube
{
    public class LavaDropHitLava : MonoBehaviour
    {
        [SerializeField] private int lavaLayerIndex;
        [SerializeField] private PlayAudioOnInteraction fromAudioScript;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != lavaLayerIndex) return;
            fromAudioScript.PlayonAction();
        }
    }
}
