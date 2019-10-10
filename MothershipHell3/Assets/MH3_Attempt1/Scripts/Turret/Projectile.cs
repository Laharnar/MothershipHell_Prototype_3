using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Directional movement with rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // on collision data.
    public int dealDamage = 1;

    public void DealDamageToStats(Stats hp)
    {
        hp.ReduceHp(dealDamage);
    }
}
