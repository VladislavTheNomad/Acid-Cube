using UnityEngine;

public class BulletMoving : MonoBehaviour
{

    [SerializeField] public float speedOfBullet;
    private BulletPoolingDartTrap bulletPool;

    private void Awake()
    {
        bulletPool = FindAnyObjectByType<BulletPoolingDartTrap>();
    }

    void FixedUpdate()
    {
        transform.position += transform.up * speedOfBullet * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            bulletPool.RemoveBulletToPool(gameObject);
        }

    }
}
