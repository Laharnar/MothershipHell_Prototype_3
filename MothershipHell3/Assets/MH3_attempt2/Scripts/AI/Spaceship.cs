using System;
using System.Collections;
using UnityEngine;
public class Spaceship : STANDPhysicsMono, IAITControllable, IPooling {


    // scene
    [SerializeField] Rigidbody2D _rig;
    // instance 
    [SerializeField] Vector2 _localMoveDir;
    // data
    [SerializeField] int _alliance=-2;
    [SerializeField] bool inheritAllianceFromAITTarget = false;
    [SerializeField] int _spaceshipTypeFilter;
    [SerializeField] float _health = 1;
    [SerializeField] float _dmgModifier = 1;

    [SerializeField] float _moveSpeed=1f;

    public int Alliance { get => _alliance; }
    public float Health { get => _health; private set => _health = value; }
    public string PoolingGroupTag { get => _shipType; }

    [SerializeField] string _shipType;

    public void AITMoveTo(Vector2 point)
    {
        transform.up = (point - (Vector2)transform.position).normalized;
        _localMoveDir = Vector2.up;
    }

    void InheritAlliance()
    {

        if (inheritAllianceFromAITTarget)
        {
            AITTarget ait = GetComponentInParent<AITTarget>();
            if (ait)
            {
                _alliance = ait.Alliance;
            }
            else
            {
                Debug.Log("No AITTarget component in parent to inherit alliance from. Required for correct bullet creation.");
            }
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (_rig == null) _rig = GetComponent<Rigidbody2D>();
    }
    protected override void Preloader()
    {
        base.Preloader();
        InheritAlliance();
    }
    

    // dmg filtering
    internal bool CanRecieveDmgFrom(NormalBullet bullet)
    {
        return bullet.Alliance != Alliance;
    }

    internal void DoDamage(int dmg)
    {
        Debug.Log(string.Format("do dmg to ship "+ dmg));
        // dmg hp
        Health -= dmg * _dmgModifier;
        if (Health <= 0)
        {
            DestroyObj();
        }
    }
    

    protected override void OnPhysicsUpdate()
    {
        _rig.MovePosition(LocalPos + (Vector2)transform.TransformDirection(_localMoveDir) * Time.fixedDeltaTime*_moveSpeed);
    }

    protected override void OnTriggerIn2D(Collider2D col)
    {
        //.
    }

    protected override void DestroyObj()
    {
        if (this.TryGetUniqueClass<Pooling>())
        {
            // pool object, and disable every child
            Debug.Log("pool destroy "+gameObject);
            this.LastResult<Pooling>().DestroyPooledObject(PoolingGroupTag, gameObject, this);
        }
        else base.DestroyObj();
    }

    public void OnPooledReady()
    {
        InheritAlliance();
        IsLocked = false;
    }

    public void OnPooledStandby()
    {
        IsLocked = true;
        _localMoveDir = Vector2.zero;
    }
}
