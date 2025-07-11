using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace AcidCube
{
    public class FlameAnimation : MonoBehaviour
    {
        private Light flameLight;


        [SerializeField] private float timeForFlameLoop = 2f;

        private void Start()
        {
            flameLight = GetComponent<Light>();
            flameLight.intensity = 5f;
        }

        private void Update()
        {
            if (flameLight.intensity <= 5f /*&& !isIncreasing*/)
            {
                StartCoroutine(IntensityChangingTo(20f));
            }

            if (flameLight.intensity >= 20f/* && !isDecreasing*/)
            {
                StartCoroutine(IntensityChangingTo(5f));
            }
        }

        private IEnumerator IntensityChangingTo(float finalIntensity)
        {
            float startIntensity = flameLight.intensity;
            float elapsedTime = 0f;
            float progress;
            while (elapsedTime < timeForFlameLoop)
            {
                float randomStep = UnityEngine.Random.Range(-0.02f, 0.05f);
                elapsedTime += Time.deltaTime + randomStep;
                progress = elapsedTime / timeForFlameLoop;
                flameLight.intensity = Mathf.Lerp(startIntensity, finalIntensity, progress);
                yield return null;
            }
        }
    }
}
