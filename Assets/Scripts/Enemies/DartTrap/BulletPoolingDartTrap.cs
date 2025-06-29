using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolingDartTrap : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] int countOfBullets;
    private List<GameObject> pooledBullets;

    private GameObject newBullet;

    private void Awake()
    {
        pooledBullets = new List<GameObject>();

        for (int i = 0; i < countOfBullets; i++)
        {
            newBullet = Instantiate(bullet);
            newBullet.SetActive(false);
            pooledBullets.Add(newBullet);
        }
    }

    public GameObject GetBulletFromPool()
    {
        for (int i =0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    public void RemoveBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }

}
