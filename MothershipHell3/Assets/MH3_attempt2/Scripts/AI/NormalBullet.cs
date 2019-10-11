using System;
using UnityEngine;

public class NormalBullet : STANDPhysicsMono, IPooling {

    bool move;
    [SerializeField] int dmg = 1;
    [SerializeField] int _alliance;
    public int Alliance { get => _alliance; }
    public string PoolingGroupTag { get => "Bullets"; }
    [Range(0, 200)] [SerializeField] float lifeTime = 1;
    float startLifetime;

    protected override void OnIsLockedUpdate()
    {
        //.
    }

    protected override void OnIsUnlockedUpdate()
    {
        move = true;
        if (Time.time > startLifetime+lifeTime)
        {
            Debug.Log("Bullet timed out - lifetime: "+lifeTime);
            DestroyObj();
        }
    }

    protected override void OnPhysicsUpdate()
    {
        rig.MovePosition(LocalPos+Vector2.up);
    }

    protected override void Initialize()
    {
        base.Initialize();
        move = false;

    }

    protected override void Preloader()
    {
        //.
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

    public void OnPooledReady()
    {
        IsLocked = false;
        updatePhysics = true;
        startLifetime = Time.time;
    }

    public void OnPooledStandby()
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
