using UnityEngine;

[CreateAssetMenu(fileName = "RangeCheck", menuName = "AI/InRange", order = 1)]
public class AQRangeCheck : AQNode {
    public float range = 5;

    public override AQResult Execute()
    {
        Transform target = AQAccess.GetData("Target") as Transform;
        ShipAI source = AQAccess.GetData("Source") as ShipAI;
        Transform root = AQAccess.GetData("Root") as Transform;
        source.DrawGizmoSphere(root.position, range);
        if (target != null)
        {
            return Vector2.Distance(target.position, root.position) < range 
                ? AQResult.Success 
                : AQResult.Fail;
        }
        return AQResult.Fail;
    }

    
}
