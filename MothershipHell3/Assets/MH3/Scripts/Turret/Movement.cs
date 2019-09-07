using System;
using UnityEngine;
/// <summary>
/// Directional movement with rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    [SerializeField] Vector2 direction = Vector2.up;
    [SerializeField] float speed = 10f;
    Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.MovePosition(rig.position + (Vector2)transform.InverseTransformDirection(direction) * Time.fixedDeltaTime * speed);
    }

    internal void FlyForward()
    {
        direction = Vector2.up;
    }
}