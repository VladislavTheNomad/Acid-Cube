using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class DartTrap : MonoBehaviour
    {
        [SerializeField] public GameObject firingModule;
        [SerializeField] private PlayAudioOnInteraction fromAudioScript;
        [SerializeField] private float fireDelay = 2f;

        private BulletPoolingDartTrap bulletPool;
        private bool fireIsReady = true;

        private void Awake()
        {
            // O(n) non-optimal method
            bulletPool = FindAnyObjectByType<BulletPoolingDartTrap>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            if (fireIsReady)
            {
                fireIsReady = false;
                StartCoroutine(Recharge());
                var newBullet = bulletPool.GetBulletFromPool();

                newBullet.transform.SetLocalPositionAndRotation(
                    firingModule.transform.position,
                    firingModule.transform.rotation);
                fromAudioScript.PlayonAction();
            }
        }

        private IEnumerator Recharge()
        {
            yield return new WaitForSeconds(fireDelay);
            fireIsReady = true;
        }
    }
}