using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AcidCube
{
    public class CrystalTrap : MonoBehaviour
    {
        //settings

        [SerializeField] private float timeOfDestroying = 1f;
        [SerializeField] private List<GameObject> crystalShards;

        // Own

        private List<Vector3> startPositions;
        private List<Quaternion> startRotations;
        private List<Vector3> endPositions;
        private List<Quaternion> endRotations;
        private int dice;
        private bool isActivated;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                crystalShards.Add(child.gameObject);
            }

            startPositions = new List<Vector3>();
            endPositions = new List<Vector3>();
            startRotations = new List<Quaternion>();
            endRotations = new List<Quaternion>();

            for (int i = 0; i < crystalShards.Count; i++)
            {
                if (crystalShards[i] != null)
                {
                    startPositions.Add(crystalShards[i].transform.localPosition);
                    startRotations.Add(crystalShards[i].transform.rotation);
                    dice = Random.Range(0, 2);
                    if (dice == 0)
                    {
                        endPositions.Add(startPositions[i] + new Vector3(5f, Random.Range(0f, 5f), 0));
                    }
                    else
                    {
                        endPositions.Add(startPositions[i] + new Vector3(-5f, Random.Range(0f, 5f), 0));
                    }

                    endRotations.Add(Quaternion.Euler(startRotations[i].x + Random.Range(-180f, 180f), startRotations[i].y + Random.Range(-180f, 180f), startRotations[i].z + Random.Range(-180f, 180f)));
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() && isActivated == false)
            {
                isActivated = true;
                Apart();
                StartCoroutine(TimeBeforeDestoy());
            }
        }

        [ContextMenu("Apart")]
        private void Apart()
        {

            for (int i = 0; i < crystalShards.Count; i++)
            {
                if (crystalShards[i] != null)
                {
                    StartCoroutine(Moving(i));
                }
            }
        }

        private IEnumerator TimeBeforeDestoy()
        {
            yield return new WaitForSeconds(timeOfDestroying + 1f);
            Destroy(gameObject);
        }

        private IEnumerator Moving(int index)
        {
            float elapsedTime = 0f;

            Vector3 startPos = startPositions[index];
            Vector3 endPos = endPositions[index];

            Quaternion startRot = startRotations[index];
            Quaternion endRot = endRotations[index];

            float progress;

            while (elapsedTime < timeOfDestroying && crystalShards[index] != null)
            {
                elapsedTime += Time.deltaTime;
                progress = elapsedTime / timeOfDestroying;
                crystalShards[index].transform.localPosition = Vector3.Lerp(startPos, endPos, progress);
                crystalShards[index].transform.rotation = Quaternion.Lerp(startRot, endRot, progress);
                yield return null;
            }
            Destroy(crystalShards[index]);
        }
    }

}