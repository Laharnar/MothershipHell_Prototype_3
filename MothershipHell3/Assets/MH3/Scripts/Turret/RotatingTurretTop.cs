using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTurretTop : MonoBehaviour
{
    [SerializeField] Vector3 aimAtRelative;
    [SerializeField] float degrees;
    [SerializeField] float smoothingFactor = 1f;

    [SerializeField] bool useSlerp = false;
    Quaternion newRot;

    public bool IsIdle { get; internal set; }

    private void Update()
    {
        if (degrees < -360) degrees += 360;
        if (degrees > 360) degrees -= 360;

        // for debugging, update aim atRelative.
        aimAtRelative.x = Mathf.Cos(degrees * Mathf.Deg2Rad);
        aimAtRelative.y = Mathf.Sin(degrees * Mathf.Deg2Rad);

        Quaternion newRot;
        if (useSlerp)
        {
            newRot = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.Euler(0, 0, degrees),
                Time.deltaTime * smoothingFactor);
        }
        else
        {
            //Quaternion.LookRotation(Vector3.forward, directionToTarget), 
            newRot = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(0, 0, degrees),
                Time.deltaTime * smoothingFactor);
        }
        transform.localRotation = newRot;
    }

    public void TurnToPoint(Vector2 point)
    {
        aimAtRelative = (point - (Vector2)transform.position).normalized;
        degrees = Mathf.Atan2(aimAtRelative.y, aimAtRelative.x) * Mathf.Rad2Deg;
    }

    public void TurnInDirection(Vector2 dir)
    {
        aimAtRelative = dir;
        degrees = Mathf.Atan2(aimAtRelative.y, aimAtRelative.x) * Mathf.Rad2Deg;
    }

    public void TurnToDegrees(float degToUp)
    {
        aimAtRelative.x = Mathf.Cos(degToUp * Mathf.Deg2Rad);
        aimAtRelative.y = Mathf.Sin(degToUp * Mathf.Deg2Rad);
        degrees = degToUp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, aimAtRelative);
        Gizmos.DrawRay(transform.position, aimAtRelative);
    }
}
