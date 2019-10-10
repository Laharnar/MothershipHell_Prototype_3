using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "AI/Fire", order = 1)]
public class AQFire : AQDecoratorNode {
    public FireCommand fireType = FireCommand.FireOneReady;

    public override AQResult Execute()
    {
        TurretAPI api = AQAccess.GetData("TurretControl") as TurretAPI;

        if (child==null || child.Execute() == AQResult.Success)
        {
            Debug.Log("AQFIRe");
            api.Fire(fireType);
            return AQResult.Success;
        }
        return AQResult.Fail;
    }
}