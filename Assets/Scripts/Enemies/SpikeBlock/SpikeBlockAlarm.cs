using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlockAlarm : MonoBehaviour
{

    [SerializeField] private GameObject[] spikes;

    private float currentY;
    private float startPositionY;
    private float endPositionY;
    [SerializeField] private float movePositionY;
    [SerializeField] private float timeForSpikeMoving;


    private void Awake()
    {
        startPositionY = spikes[0].transform.localPosition.y;
        endPositionY = startPositionY + movePositionY;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in spikes)
            {
                StartCoroutine(SpikeMoving(item));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in spikes)
            {
                StartCoroutine(SpikeClosing(item));
            }

        }
    }


    IEnumerator SpikeMoving(GameObject item)
    {
        float elapsedTimeForSpike = 0f;
        float nowPositionY = item.transform.localPosition.y; // get local Y 
        while (elapsedTimeForSpike < timeForSpikeMoving)
        {
            elapsedTimeForSpike += Time.deltaTime;
            currentY = Mathf.Lerp(nowPositionY, endPositionY, elapsedTimeForSpike/timeForSpikeMoving);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, currentY, item.transform.localPosition.z);
            yield return null;
        }
        
    }

    IEnumerator SpikeClosing(GameObject item)
    {
        float elapsedTimeForSpike = 0f;
        float nowPositionY = item.transform.localPosition.y; // get local Y 
        while (elapsedTimeForSpike < timeForSpikeMoving)
        {
            elapsedTimeForSpike += Time.deltaTime;
            currentY = Mathf.Lerp(nowPositionY, startPositionY, elapsedTimeForSpike / timeForSpikeMoving);
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, currentY, item.transform.localPosition.z);
            yield return null;
        }
    }

}
