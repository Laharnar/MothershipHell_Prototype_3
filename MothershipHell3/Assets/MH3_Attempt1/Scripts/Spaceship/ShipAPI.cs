using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAPI : MonoBehaviour, IAQAccessible {
    [SerializeField] Movement movement;
    [SerializeField] RotatingTurretTop rotation;

    // [SerializeField] Stats stats;
    [SerializeField] TurretAPI turretControl;// rotation is handled inside turret API
    public TurretAPI TurretControl { get => turretControl; }// rotation is handled inside turret API

    public Transform Obj { get => transform; }
    public Vector3 AimingToLookInDir { get => rotation.Direction; }
    public float ShipDegrees { get => rotation.Degrees; }

    bool init = false;

    public void Fly(Vector2 point)
    {
        movement.FlyForward();
        turretControl.TurnTowardsEmptySpace(point);
    }

    public void Track(Transform target, bool move =true, bool rotate = true)
    {
        if(move) movement.FlyForward();
        if(rotate) rotation.TurnToPoint(target.localPosition);
    }


    
}
