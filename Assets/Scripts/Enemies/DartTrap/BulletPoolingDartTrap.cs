using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace AcidCube
{
    public class BulletPoolingDartTrap : MonoBehaviour
    {

        [SerializeField] GameObject bulletPrefab;

        private ObjectPool<GameObject> bulletsPool;

        private void Awake()
        {
            bulletsPool = new ObjectPool<GameObject>(CreateNewBullet);
        }

        private GameObject CreateNewBullet()
        {
            var newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            return newBullet;
        }

        public GameObject GetBulletFromPool()
        {
            var bullet = bulletsPool.Get();
            bullet.SetActive(true);
            return bullet;
        }

        public void ReleaseBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            bulletsPool.Release(bullet);
        }
    }
}
