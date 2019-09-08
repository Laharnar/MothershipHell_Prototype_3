using UnityEngine;
[CreateAssetMenu(fileName ="And", menuName ="AI/AND", order = 1)]
public class AQAndConditionNode : AQListNode {
    // true when all pass, wait and fail are returned.

    public override AQResult Execute()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            AQResult result = conditions[i].Execute();
            if (result == AQResult.Fail)
                return AQResult.Fail;
            if (result == AQResult.Wait)
                return AQResult.Wait;
        }
        return AQResult.Success;
    }
}
