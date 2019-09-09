using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAPI : MonoBehaviour, IAQAccessible {
    [SerializeField] Movement movement;
    // [SerializeField] Stats stats;
    [SerializeField] TurretAPI turretControl;// rotation is handled inside turret API
    public TurretAPI TurretControl { get => turretControl; }// rotation is handled inside turret API

    public Transform Obj { get => transform; }

    // tmp, TODO: remove later
    public Transform target;

    private void Start()
    {
        Track(target);
    }

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
