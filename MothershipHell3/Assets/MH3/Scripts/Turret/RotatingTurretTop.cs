using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTurretTop : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float smoothingFactor = 1f;
    [SerializeField] bool useSlerp = false;

    [Header("Runtime")]
    [SerializeField] Vector3 aimAtRelativeDir;// dir
    [SerializeField] float degrees;
    Quaternion newRot;

    public bool IsIdle { get; internal set; }
    public float Degrees { get => degrees; }
    public Vector2 Direction { get => aimAtRelativeDir; }

    RotatingTurretTop[] parentChain;

    private void Start()
    {
        parentChain = gameObject.GetComponentsInParent<RotatingTurretTop>();
    }

    private void Update()
    {
        if (degrees < -360) degrees += 360;
        if (degrees > 360) degrees -= 360;

        // for debugging, update aim atRelative.
        //aimAtRelativeDir.x = Mathf.Cos(degrees * Mathf.Deg2Rad);
        //aimAtRelativeDir.y = Mathf.Sin(degrees * Mathf.Deg2Rad);

        Quaternion newRot = CalculateRotation(Time.deltaTime);
        transform.localRotation = newRot;
    }

    private Quaternion CalculateRotation(float deltaTime)
    {
        Quaternion newRot;
        if (useSlerp)
        {
            newRot = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.Euler(0, 0, degrees),
                deltaTime * smoothingFactor);
        }
        else
        {
            //Quaternion.LookRotation(Vector3.forward, directionToTarget), 
            newRot = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(0, 0, degrees),
                deltaTime * smoothingFactor);
        }

        return newRot;
    }

    float SumRotation()
    {
        float sum = 0;
        for (int i = 0; i < parentChain.Length; i++)
        {
            sum += parentChain[i].degrees;
        }
        return sum;
    }
    public void TurnToPoint(Vector2 point)
    {
        Q.Log("Turning to point, this" + point + " " + (Vector2)transform.position);
        aimAtRelativeDir = (point - (Vector2)transform.position).normalized;
        degrees = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        degrees = SumRotation();
    }
    public void TurnToRelativePoint(Vector2 point)
    {
        Q.Log("Turning to point, this"+ point +" "+ (Vector2)transform.position);
        aimAtRelativeDir = (point - (Vector2)transform.position).normalized;
        Vector2 vec = transform.InverseTransformPoint(point);
        degrees = Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
        degrees = SumRotation();
    }

    public void TurnInDirection(Vector2 dir)
    {
        aimAtRelativeDir = dir.normalized;
        degrees = Mathf.Atan2(aimAtRelativeDir.y, aimAtRelativeDir.x) * Mathf.Rad2Deg;
        degrees = SumRotation();
    }

    public void TurnToDegrees(float degToUp)
    {
        aimAtRelativeDir.x = Mathf.Cos(degToUp * Mathf.Deg2Rad);
        aimAtRelativeDir.y = Mathf.Sin(degToUp * Mathf.Deg2Rad);
        degrees = degToUp;
        degrees = SumRotation();
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
