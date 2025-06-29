using System.Collections;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public GameObject firingModule;
    private BulletPoolingDartTrap bulletPool;

    private GameObject newBullet;

    private RaycastHit hit;
    [SerializeField] LayerMask groundLayer;
    public bool isShot = false;

    private Coroutine nowFiringCoroutine;

    private void Awake()
    {
        bulletPool = FindAnyObjectByType<BulletPoolingDartTrap>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float distanceToPlayer = Vector3.Distance(transform.position, other.transform.position);
            Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, distanceToPlayer, groundLayer);
            Debug.DrawRay(transform.position, other.transform.position - transform.position, Color.blue, 10f);
            if (hit.collider == null && !isShot) // Check if the collider property of RaycastHit is null
            {
                nowFiringCoroutine = StartCoroutine(Firing());

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nowFiringCoroutine != null)
            {
                StopCoroutine(nowFiringCoroutine);
                nowFiringCoroutine = null;
            }
        }
    }


    IEnumerator Firing()
    {
        while (true)
        {
            newBullet = bulletPool.GetBulletFromPool();
            newBullet.transform.position = firingModule.transform.position;
            newBullet.transform.rotation = firingModule.transform.rotation;
            newBullet.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }
}
