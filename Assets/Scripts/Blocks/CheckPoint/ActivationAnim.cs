using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class ActivationAnim : MonoBehaviour
    {
        private Renderer checkPoint_Renderer;
        private Material checkPointMaterial;
        private Color emissionColor = Color.white;
        private Color currentColor;
        private float intensity = 5f;
        private float currentIntensity;
        private float timeForEmissionTransition = 1f;



        private void Awake()
        {
            checkPoint_Renderer = GetComponent<Renderer>();
            checkPointMaterial = checkPoint_Renderer.material;
            Color finalColor = emissionColor * intensity;
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
            while(elapsedTime < timeForEmissionTransition)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / timeForEmissionTransition;
                currentIntensity = intensity * (progress);
                currentColor = emissionColor * currentIntensity;
                checkPointMaterial.SetColor("_EmissionColor", currentColor);
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
                currentColor = emissionColor * currentIntensity;
                checkPointMaterial.SetColor("_EmissionColor", currentColor);
                yield return null;
            }
        }
    }
}
