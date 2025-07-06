using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class DartTrap : MonoBehaviour
    {
        //[SerializeField] private GameObject bullet;
        [SerializeField] public GameObject firingModule;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] private float fireDelay = 2f;

        private BulletPoolingDartTrap bulletPool;
        private Coroutine nowFiringCoroutine;
        private bool fireIsReady = true;

        private void Awake()
        {
            // O(n) non-optimal method
            bulletPool = FindAnyObjectByType<BulletPoolingDartTrap>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>() && nowFiringCoroutine != null) return;
            
            //float distanceToPlayer = Vector3.Distance(transform.position, other.transform.position);
            //Vector3 deltaToPlayerFromObject = other.transform.position - firingModule.transform.position;
            
            
            //bool isAnyhit = Physics.Raycast(transform.position, deltaToPlayerFromObject, distanceToPlayer, groundLayer);

            // NOTE: For tests  
            //Debug.DrawRay(transform.position, deltaToPlayerFromObject, Color.blue, 10f);
            nowFiringCoroutine = StartCoroutine(Firing());
            //if (!isAnyhit)
            //{
            //    nowFiringCoroutine = StartCoroutine(Firing());
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;
            
            if(nowFiringCoroutine != null) StopCoroutine(nowFiringCoroutine);
            nowFiringCoroutine = null;
        }

        private IEnumerator Firing()
        {
            while (true)
            {
                if (fireIsReady)
                {
                    fireIsReady = false;
                    StartCoroutine(Recharge());
                    var newBullet = bulletPool.GetBulletFromPool();

                    newBullet.transform.SetLocalPositionAndRotation(
                        firingModule.transform.position,
                        firingModule.transform.rotation);
                }
                yield return new WaitForSeconds(fireDelay);
            }
        }

        private IEnumerator Recharge()
        {
            yield return new WaitForSeconds(fireDelay);
            fireIsReady = true;
        }
    }
}