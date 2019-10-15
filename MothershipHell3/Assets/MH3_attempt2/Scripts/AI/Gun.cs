using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Firing. WITHOUT rotation.
/// </summary>
public class Gun : BasicMono, IPooling
{
    // firing
    // instance data
    bool _canFire = true;
    public bool CanFire { get => _canFire; set => _canFire = value; }// high level lock.
    public bool BlockFire { get; set; } // low level lock. tells us if gun was locked from internal code.

    [SerializeField] bool inheritAllianceFromAITTarget = false;
    int _alliance = -2;

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

    // Allows bullets to get gun's alliance.
    int _allianceAssignedToBullets;
    private static Gun LastGunThatSpawned;
    public static int AllianceOfLastGunThatSpawned {
        get => LastGunThatSpawned._allianceAssignedToBullets;
    }

    public string PoolingGroupTag { get; }

    void InheritAlliance()
    {

        if (inheritAllianceFromAITTarget)
        {
            AITTarget ait = GetComponentInParent<AITTarget>();
            if (ait)
            {
                _allianceAssignedToBullets = ait.Alliance;
                _alliance = ait.Alliance;
            }
            else
            {
                Debug.Log("No AITTarget component in parent to inherit alliance from. Required for correct bullet creation.");
            }
        }
    }

    protected override void Preloader()
    {
        base.Preloader();

        InheritAlliance();

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

    protected override void OnIsUnlockedUpdate()
    {
        AIFFire();
    }

    void AIFFire()
    {
        if (CanFire && !IsLocked)
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

    public void OnPooledCreated()
    {
        IsLocked = false;
        InheritAlliance();
    }

    public void OnPooledDestroyed()
    {
        IsLocked = true;
    }
}
