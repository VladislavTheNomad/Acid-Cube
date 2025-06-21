using System;
using System.Collections;
using UnityEngine;

public class FlameAnimation : MonoBehaviour
{
    private Light flameLight;
    private bool isIncreasing = false;
    private bool isDecreasing = false;
    [SerializeField] private float timeForFlameLoop = 2f;

    void Start()
    {
        flameLight = GetComponent<Light>();
        flameLight.intensity = 5f;
    }

    void Update()
    {
        if(flameLight.intensity <= 5f && !isIncreasing)
        {
            isIncreasing = true;
            StartCoroutine(IntensityIncreasing());
        }

        if (flameLight.intensity >= 20f && !isDecreasing)
        {
            isDecreasing = true;
            StartCoroutine(IntensityDecreasing());
        }
    }

    IEnumerator IntensityDecreasing()
    {
        float startIntensity = flameLight.intensity;
        float elapsedTime = 0f;
        while (elapsedTime < timeForFlameLoop)
        {
            float randomStep = UnityEngine.Random.Range(-0.02f, 0.05f);
            elapsedTime += Time.deltaTime + randomStep;
            flameLight.intensity = Mathf.Lerp(startIntensity, 5, elapsedTime / timeForFlameLoop);
            yield return null;
        }
        isDecreasing = false;
    }

    IEnumerator IntensityIncreasing()
    {
        float startIntensity = flameLight.intensity;
        float elapsedTime = 0f;
        while (elapsedTime < timeForFlameLoop)
        {
            float randomStep = UnityEngine.Random.Range(-0.02f, 0.05f);
            elapsedTime += Time.deltaTime + randomStep;
            flameLight.intensity = Mathf.Lerp(startIntensity, 20, elapsedTime / timeForFlameLoop);
            yield return null;
        }
        isIncreasing = false;
    }
}
