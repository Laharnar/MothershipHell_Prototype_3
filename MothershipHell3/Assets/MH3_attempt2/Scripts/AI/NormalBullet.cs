using System;
using UnityEngine;

public interface IAllianceInheritance {
    int Alliance { get; }

    InheritAllianceFrom inheritFrom { get; }
}

public class NormalBullet : STANDPhysicsMono, IPooling, IAllianceInheritance {

    Vector2 localMoveDir;

    [SerializeField] int dmg = 1;
    [SerializeField] int _alliance;
    public int Alliance { get => _alliance; }
    public string PoolingGroupTag { get => "Bullets"; }

    public InheritAllianceFrom inheritFrom { get => InheritAllianceFrom.Gun; }

    [Range(0, 200)] [SerializeField] float lifeTime = 1;
    float startLifetime;

    // gun specifi
    const bool INHERITALLIANCEFROMGUN = true;

    protected override void OnIsLockedUpdate()
    {
        //.
    }

    protected override void OnIsUnlockedUpdate()
    {
        if (Time.time > startLifetime+lifeTime)
        {
            Debug.Log("Bullet timed out - lifetime: "+lifeTime);
            DestroyObj();
        }
    }

    protected override void OnPhysicsUpdate()
    {
        localMoveDir = transform.up;
        rig.MovePosition(LocalPos + localMoveDir);
        //rig.MovePosition(LocalPos + (Vector2)transform.TransformDirection(localMoveDir));
    }

    protected override void OnTriggerIn2D(Collider2D col)
    {
        Spaceship ship = col.GetComponentInParent<Spaceship>();
        if (ship && ship.CanRecieveDmgFrom(this))
        {
            ship.DoDamage(dmg);
            DestroyObj();
        }
    }

    public void OnPooledCreated()
    {
        IsLocked = false;
        updatePhysics = true;
        startLifetime = Time.time;
        _alliance = Gun.AllianceOfLastGunThatSpawned;
    }

    public void OnPooledDestroyed()
    {
        IsLocked = true;
        updatePhysics = false;
    }

    protected override void DestroyObj()
    {
        if (this.TryGetUniqueClass<Pooling>())
        {
            this.LastResult<Pooling>().DestroyPooledObject(PoolingGroupTag, gameObject, this);
        }
        else base.DestroyObj();
    }
}
