using AcidCube;
using UnityEngine;

namespace AcidCube
{
    public class Bullet : MonoBehaviour
    {

        [SerializeField] public float speedOfBullet;

        private BulletPoolingDartTrap bulletPool;

        private void Awake()
        {
            bulletPool = FindAnyObjectByType<BulletPoolingDartTrap>();
        }

        void FixedUpdate()
        {
            transform.position += transform.up * (speedOfBullet * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                bulletPool.ReleaseBullet(gameObject);
            }

        }
    }
}
