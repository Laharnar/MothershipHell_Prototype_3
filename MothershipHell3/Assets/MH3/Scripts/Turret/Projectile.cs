using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Directional movement with rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 direction = Vector2.up;
    [SerializeField] float speed = 10f;
    Rigidbody2D rig;

    // on collision data.
    public int dealDamage = 1;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.MovePosition(rig.position + (Vector2)transform.InverseTransformDirection(direction)*Time.fixedDeltaTime*speed);
    }

    public void DealDamageToStats(Stats hp)
    {
        hp.ReduceHp(dealDamage);
    }
}
