using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAPI : MonoBehaviour
{
    [SerializeField] Movement movement;
    // [SerializeField] Stats stats;
    [SerializeField] TurretAPI turretControl;// rotation is handled inside turret API
    
    public void Fly(Vector2 point)
    {
        movement.FlyForward();
        turretControl.TurnTowardsEmptySpace(point);
    }

    public void Track(Transform target)
    {
        movement.FlyForward();
        turretControl.AssignTarget(target, TargetType.Moving);
    }
}
