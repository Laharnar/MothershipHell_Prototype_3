using UnityEngine;

/// <summary>
/// Success <- -> fail, keep the rest
/// </summary>
[UnityEngine.CreateAssetMenu(fileName ="ToggleSF", menuName ="AI/ToggleSF",order = 1)]
public class AQToggleSuccessAndFailNode : AQDecoratorNode {
    public override AQResult Execute()
    {
        // converts, or keeps value
        AQResult result = child.Execute();
        if (result == AQResult.Fail)
        {
            return AQResult.Success;
        }
        if (result == AQResult.Success)
        {
            return AQResult.Fail;
        }
        return result;
    }
}
