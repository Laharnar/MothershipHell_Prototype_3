using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringBattery : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] float fireRate = 1f;
    [SerializeField] bool readyOnStart = true;

    [SerializeField] bool readyToFire  = false;
    [SerializeField] uint allianceFilter = 0;
    public bool holdFire = false;

    public bool IsReady { get => readyToFire; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Firing());
    }

    IEnumerator Firing()
    {
        if (readyOnStart && bulletPrefab && spawnPoint && !holdFire)
            SpawnBullet(bulletPrefab);

        while (true)
        {
            
            readyToFire = false;
            if (fireRate != 0)
                yield return new WaitForSeconds(fireRate);
            else yield return null;
            readyToFire = true;

            while (holdFire)
                yield return null;

            if (bulletPrefab && spawnPoint)
            {
                SpawnBullet(bulletPrefab);
                readyToFire = false;
            }
        }
    }

    private void SpawnBullet(Transform prefab)
    {
        Transform bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<OnCollision>().SetMask(allianceFilter);
    }
}
