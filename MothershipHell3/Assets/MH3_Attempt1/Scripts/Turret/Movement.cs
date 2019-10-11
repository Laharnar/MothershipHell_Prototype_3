using System;
using UnityEngine;
/// <summary>
/// Directional movement with rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    bool move = true;
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
        if (move)
        {
            rig.MovePosition(rig.position + (Vector2)transform.TransformDirection(direction) * Time.fixedDeltaTime * speed);
        }
    }

    internal void FlyForward()
    {
        direction = Vector2.up;
    }
}