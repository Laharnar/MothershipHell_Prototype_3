/// <summary>
/// fail ->success, keep the rest
/// </summary>
[UnityEngine.CreateAssetMenu(fileName ="FtoSConvert", menuName ="AI/FtoSConvert",order = 1)]
public class AQFailToSuccessNode : AQDecoratorNode {
    public override AQResult Execute()
    {
        // converts, or keeps value
        AQResult result = child.Execute();
        if (result == AQResult.Fail)
        {
            return AQResult.Success;
        }
        return result;
    }
}
