using UnityEngine;

/// <summary>
/// Rotation.
/// </summary>
public class Turret : STANDSelectableMono, IPooling, IAITControllable {

    // scene data
    [SerializeField] Gun[] guns;

    // instance data
    [SerializeField] int _alliance=-2;

    // data
    [SerializeField] bool inheritAllianceFromAITTarget = false;
    [SerializeField] string _turretType;
    public string PoolingGroupTag { get => _turretType; }

    public void AIBFire()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].CanFire = true;
        }
    }

    public void AIBHoldFire()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].CanFire = false;
        }
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

    public void OnPooledCreated()
    {
        _alliance = GetComponentInParent<IAllianceInheritance>().Alliance;
        IsLocked = false;
        AIBFire();
    }

    public void OnPooledDestroyed()
    {
        IsLocked = true;
        AIBHoldFire();
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
            _alliance = GetComponentInParent<IAllianceInheritance>().Alliance;
            RegisterToDrag();
        }
    }

    protected override void DestroyObj()
    {
        if (this.TryGetUniqueClass<Pooling>())
        {
            // pool object, and disable every child
            Debug.Log("pool destroy " + gameObject);
            if (this.LastResult<Pooling>().DestroyPooledObject(PoolingGroupTag, gameObject, this) == false)
            {
                Debug.Log("Failed to pool destroy. "+PoolingGroupTag+" < "+name);
                base.DestroyObj();
            }
        }
        else base.DestroyObj();
    }

    public void AITAimMoveTo(Vector2 point)
    {
        transform.up = point - (Vector2)transform.position;
    }
}
