using UnityEngine;

public class AQLogNode : AQNode {
    public string msg;
    public override AQResult Execute()
    {
        Debug.Log("Log Node :: "+ msg);
        return AQResult.Success;
    }
}
