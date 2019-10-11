using System;
using System.Collections;
using UnityEngine;

public class Spaceship : STANDSelectableMono {

    Vector2 localMoveDir;

    // scene
    [SerializeField] Rigidbody2D _rig;
    // instance 
    [SerializeField] int _alliance;
    // data
    [SerializeField] float _health = 1;
    [SerializeField] float _dmgModifier = 1;

    public int Alliance { get => _alliance; }
    public float Health { get => _health; set => _health = value; }

    public void MoveTo(Vector2 point)
    {
        transform.up = (point - (Vector2)transform.position).normalized;
        localMoveDir = Vector2.up;
    }

    protected override void OnIsUnlockedUpdate()
    {
        _rig.MovePosition(LocalPos + localMoveDir);
    }

    protected override void OnIsLockedUpdate()
    {
        // nothing.
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
            Destroy(gameObject);
        }
    }
}
