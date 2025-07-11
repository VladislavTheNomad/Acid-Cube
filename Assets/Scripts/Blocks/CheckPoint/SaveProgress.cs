using UnityEngine;

namespace AcidCube
{
    public class SaveProgress : MonoBehaviour
    {
        private SavingProgress saveProgressScript;

        private Vector3 pointRebirth;

        private void Awake()
        {
            pointRebirth = transform.position + new Vector3(0f, 1f, 0f);
            saveProgressScript = FindAnyObjectByType<SavingProgress>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            saveProgressScript.SavePlayerRevivingPoint(pointRebirth);
        }

    }
}
