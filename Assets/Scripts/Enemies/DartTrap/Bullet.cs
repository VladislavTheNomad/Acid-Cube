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
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                bulletPool.ReleaseBullet(gameObject);
            }
            else if (other.gameObject.GetComponent<PlayerController>())
            {
                bulletPool.ReleaseBullet(gameObject);
                GameOverMenu.instance.OpenGameOverMenu();
            }

        }
    }
}
