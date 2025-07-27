using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class TriggerBlockActivation : MonoBehaviour
    {
        private Color emissionOffColor = Color.red;
        private Color emissionOnColor = Color.green;
        private Color currentColor;

        private Renderer triggerBlockRenderer;
        private Material triggerBlockMaterial;

        private float intensity = 5f;
        private float currentIntensity;
        private float timeForEmissionTransition = 1f;



        private void Awake()
        {
            triggerBlockRenderer = GetComponent<Renderer>();
            triggerBlockMaterial = triggerBlockRenderer.material;
            triggerBlockMaterial.color = Color.red;
            Color finalColor = emissionOffColor * intensity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            StartCoroutine(EmissionUp());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            StartCoroutine(EmissionDown());
        }

        private IEnumerator EmissionUp()
        {
            float elapsedTime = 0f;
            float progress = 0f;
            while (elapsedTime < timeForEmissionTransition)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / timeForEmissionTransition;
                currentIntensity = intensity * (progress);
                currentColor = emissionOnColor * currentIntensity;
                triggerBlockMaterial.SetColor("_EmissionColor", currentColor);
                yield return null;
            }
        }

        private IEnumerator EmissionDown()
        {
            float elapsedTime = 0f;
            float progress = 0f;
            while (elapsedTime < timeForEmissionTransition)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / timeForEmissionTransition;
                currentIntensity = intensity - (elapsedTime * intensity);
                currentColor = emissionOffColor * currentIntensity;
                triggerBlockMaterial.SetColor("_EmissionColor", currentColor);
                yield return null;
            }
        }
    }
}
