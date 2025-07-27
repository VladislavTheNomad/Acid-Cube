using AcidCube;
using UnityEngine;

namespace AcidCube
{
    public class SliderBlockActivation : MonoBehaviour
    {
        [SerializeField] SliderAnimation[] sliders;

        private bool isFirstTime = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            if (isFirstTime)
            {
                isFirstTime = false;
                for (int i = 0; i < sliders.Length; i++)
                {
                    sliders[i].TurnOn();
                }
                
            }
        }
    }
}