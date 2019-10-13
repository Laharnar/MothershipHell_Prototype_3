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

    [SerializeField] bool inheritAllianceFromAITTarget = false;
    int _allianceAssignedToBullets = -2;

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

    private static Gun LastGunThatSpawned;
    public static int AllianceOfLastGunThatSpawned {
        get => LastGunThatSpawned._allianceAssignedToBullets;
    }

    protected override void OnIsLockedChange(bool isLocked)
    {
        base.OnIsLockedChange(isLocked);
        if (isLocked)
        {
            UnRegisterFromDrag();
        }
        else
        {
            RegisterToDrag();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

    }
    protected override void Preloader()
    {
        base.Preloader();

        if (inheritAllianceFromAITTarget)
        {
            AITTarget ait = GetComponentInParent<AITTarget>();
            if (ait)
            {
                _allianceAssignedToBullets = ait.Alliance;
            }
            else
            {
                Debug.Log("No AITTarget component in parent to inherit alliance from. Required for correct bullet creation.");
            }
        }

        // save bullet's tag, to use it later when creating bullets.
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
        if (optional_poolingBulletTag != null)
        {
            LastGunThatSpawned = this;
            this.GetUniqueClass<Pooling>().CreateInstance(optional_poolingBulletTag, prefabBullet, spawnPoint.position, spawnPoint.rotation);

            

        }
        else Instantiate(prefabBullet, spawnPoint.position, spawnPoint.rotation);
    }

    protected override void OnIsUnlockedUpdate()
    {
        BlockFire = false;
        AIFFire();
    }

    protected override void OnIsLockedUpdate()
    {
        BlockFire = true;
    }
}
