using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : STANDSelectableMono
{
    // firing
    // instance data
    bool _canFire = true;
    public bool CanFire { get => _canFire; set => _canFire = value; }// high level lock.
    public bool BlockFire { get; set; } // tells us if gun was locked from internal code.

    // gun
    // data
    [Range(0.001f, 10f)] [SerializeField] float fireRateFine;
    [Range(0, 200)] [SerializeField] float fireRateRough;

    [SerializeField] Transform prefabBullet;

    // scene data
    [SerializeField] Transform sceneSpawnPoint;

    // instance data
    float _fireRate;
    public float FireRate { get => _fireRate = fireRateFine + fireRateRough; }
    float nextGunFire = 0;
    // + pooling
    string optional_poolingBulletTag;

    protected new void Start()
    {
        base.Start();

        optional_poolingBulletTag = null;
        if (prefabBullet)
        {
            IPooling p = prefabBullet.GetComponent<IPooling>();
            if (p != null)
            {
                optional_poolingBulletTag = p.PoolingGroupTag;
            }
        }
        else Debug.Log("Missing bullet prefab.");
    }

    void AIFFire()
    {
        if (CanFire && !BlockFire)
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        if (Time.time >= nextGunFire)
        {
            nextGunFire = Time.time+FireRate;

            // create bullet
            SpawnBullet(prefabBullet, sceneSpawnPoint);
        }
    }

    void SpawnBullet(Transform prefabBullet, Transform spawnPoint, object extension=null)
    {
        if (optional_poolingBulletTag!=null)
            this.GetUniqueClass<Pooling>().CreateInstance("Bullets", prefabBullet, spawnPoint.position, spawnPoint.rotation);
        else Instantiate(prefabBullet, spawnPoint.position, spawnPoint.rotation);
    }

    protected override void OnIsUnlockedUpdate()
    {
        Debug.Log("unlokced");
        BlockFire = false;
        AIFFire();
    }

    protected override void OnIsLockedUpdate()
    {
        BlockFire = true;
    }
}
