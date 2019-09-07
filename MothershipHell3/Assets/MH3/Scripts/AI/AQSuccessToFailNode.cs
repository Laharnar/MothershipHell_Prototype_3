using UnityEngine;

/// <summary>
/// Converts success to fail, others are normally returned.
/// </summary>
[UnityEngine.CreateAssetMenu(fileName ="StoFConvert", menuName ="AI/StoFConvert",order = 1)]
public class AQSuccessToFailNode : AQNode {
    [SerializeField] AQNode child;
    public override AQResult Execute()
    {
        // converts, or keeps value
        AQResult result = child.Execute();
        if (result == AQResult.Success)
        {
            return AQResult.Fail;
        }
        return result;
    }
}
