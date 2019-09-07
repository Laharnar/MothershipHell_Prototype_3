using UnityEngine;

public class OnCollision:MonoBehaviour {
    [SerializeField] uint allianceFilter = 0;

    Projectile hasProjectileRef;
    [SerializeField] bool selfDestructOnFirstCollision = false;

    private void Awake()
    {
        hasProjectileRef = GetComponent<Projectile>();
    }

    public static bool DifferentAlliance(OnCollision a, OnCollision b)
    {
        return a.allianceFilter == 0 // any
            || b.allianceFilter == 0 // any
            || ((a.allianceFilter & b.allianceFilter) == 0);// check if they incompatible
        // will work for both being 0
    }

    public void OnFirstTrigger()
    {
        if (selfDestructOnFirstCollision)
        {
            Destroy(gameObject);
        }
    }

    public void SendDamage(Stats otherHp)
    {
        if (hasProjectileRef)
        {
            hasProjectileRef.DealDamageToStats(otherHp);
        }
        OnFirstTrigger();
    }

    public void SetMask(uint mask)
    {
        allianceFilter = mask;
    }
}
