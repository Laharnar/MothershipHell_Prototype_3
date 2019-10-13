using System;
using System.Collections;
using UnityEngine;
public class Spaceship : STANDPhysicsMono, IAITControllable {


    // scene
    [SerializeField] Rigidbody2D _rig;
    // instance 
    [SerializeField] Vector2 _localMoveDir;
    // data
    [SerializeField] int _alliance;
    [SerializeField] int _spaceshipTypeFilter;
    [SerializeField] float _health = 1;
    [SerializeField] float _dmgModifier = 1;

    [SerializeField] float _moveSpeed=1f;

    public int Alliance { get => _alliance; }
    public float Health { get => _health; set => _health = value; }

    public string PoolingGroupTag = "SpaceshipType1";

    public void AITMoveTo(Vector2 point)
    {
        transform.up = (point - (Vector2)transform.position).normalized;
        _localMoveDir = Vector2.up;
    }

    protected override void Preloader()
    {
        base.Preloader();
        if (_rig == null)
        {
            _rig = GetComponent<Rigidbody2D>();
        }
    }

    // dmg filtering
    internal bool CanRecieveDmgFrom(NormalBullet bullet)
    {
        return bullet.Alliance != Alliance;
    }

    internal void DoDamage(int dmg)
    {
        //.
        Debug.Log(string.Format("do dmg to ship "+ dmg));
        // dmg hp
        Health -= dmg * _dmgModifier;
        if (Health <= 0)
        {
            DestroyObj();
        }
    }

    internal void AITStop()
    {
        _localMoveDir = Vector2.zero;
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
}
