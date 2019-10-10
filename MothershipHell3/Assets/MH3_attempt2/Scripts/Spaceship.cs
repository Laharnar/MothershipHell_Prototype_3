using System.Collections;
using UnityEngine;

public class Spaceship : StandardSelectableMono {
    Vector2 localMoveDir;

    [SerializeField] Rigidbody2D rig;

    public void MoveTo(Vector2 point)
    {
        transform.up = (point - (Vector2)transform.position).normalized;
        localMoveDir = Vector2.up;
    }

    protected override void OnIsUnlocked()
    {
        rig.MovePosition(LocalPos + localMoveDir);
    }

    protected override void OnIsLocked()
    {
        // nothing.
    }

    protected override void Preloader()
    {
        base.Preloader();
        if (rig == null)
        {
            rig = GetComponent<Rigidbody2D>();
        }
    }
}
