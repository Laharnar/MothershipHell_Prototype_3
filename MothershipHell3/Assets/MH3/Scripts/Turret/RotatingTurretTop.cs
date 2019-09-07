using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTurretTop : MonoBehaviour
{
    [SerializeField] Vector3 aimAtRelativeDir;// dir
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
        aimAtRelativeDir.x = Mathf.Cos(degrees * Mathf.Deg2Rad);
        aimAtRelativeDir.y = Mathf.Sin(degrees * Mathf.Deg2Rad);

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
        aimAtRelativeDir = (point - (Vector2)transform.position).normalized;
        degrees = Mathf.Atan2(aimAtRelativeDir.y, aimAtRelativeDir.x) * Mathf.Rad2Deg;
    }

    public void TurnInDirection(Vector2 dir)
    {
        aimAtRelativeDir = dir;
        degrees = Mathf.Atan2(aimAtRelativeDir.y, aimAtRelativeDir.x) * Mathf.Rad2Deg;
    }

    public void TurnToDegrees(float degToUp)
    {
        aimAtRelativeDir.x = Mathf.Cos(degToUp * Mathf.Deg2Rad);
        aimAtRelativeDir.y = Mathf.Sin(degToUp * Mathf.Deg2Rad);
        degrees = degToUp;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {// to display this aim properly, it should be added to parent rotation.
     // which is a waste of resources, just for gizmos.
        Gizmos.DrawRay(transform.position, 
            transform.TransformDirection(aimAtRelativeDir));
        Gizmos.DrawLine(transform.position, transform.position+transform.up*2);
    }
#endif
}
