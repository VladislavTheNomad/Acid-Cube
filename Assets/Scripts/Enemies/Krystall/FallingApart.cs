using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingApart : MonoBehaviour
{
    [SerializeField] private float timeOfDestroying = 1f;
    private List<Vector3> startPositions;
    private List<Quaternion> startRotations;
    private List<Vector3> endPositions;
    private List<Quaternion> endRotations;

    private int dice;

    [SerializeField] private List<GameObject> crystallSharps;

    private void Awake()
    {
        startPositions = new List<Vector3>();
        endPositions = new List<Vector3>();
        startRotations = new List<Quaternion>();
        endRotations = new List<Quaternion>();

        for (int i = 0; i<crystallSharps.Count; i++)
        {
            if (crystallSharps[i] != null)
            {
                startPositions.Add(crystallSharps[i].transform.position);
                startRotations.Add(crystallSharps[i].transform.rotation);
                dice = Random.Range(0, 2);
                if(dice == 0)
                {
                    endPositions.Add(startPositions[i] + new Vector3(5f, Random.Range(0.2f, 5f), 0));
                }
                else
                {
                    endPositions.Add(startPositions[i] + new Vector3(-5f, Random.Range(0.2f, 5f), 0));
                }
                endRotations.Add(Quaternion.Euler(startRotations[i].x + Random.Range(-90f, 90f), startRotations[i].y + Random.Range(-90f, 90f), startRotations[i].z + Random.Range(-90f, 90f)));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Apart();
        }
    }

    [ContextMenu("Apart")]
    void Apart()
    {

        for (int i = 0; i < crystallSharps.Count; i++)
        {
            if (crystallSharps[i] != null)
            {
                StartCoroutine(Moving(i));
            }
        }
    }

    IEnumerator Moving(int index)
    {
        float elapsedTime = 0f;

        Vector3 startPos = startPositions[index];
        Vector3 endPos = endPositions[index];

        Quaternion startRot = startRotations[index];
        Quaternion endRot = endRotations[index];

        while (elapsedTime < timeOfDestroying && crystallSharps[index] != null)
        {
            elapsedTime += Time.deltaTime;
            crystallSharps[index].transform.position = Vector3.Lerp(startPos, endPos, elapsedTime/timeOfDestroying);
            crystallSharps[index].transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / timeOfDestroying);
            yield return null;
        }
        Destroy(crystallSharps[index]);
    }
}
