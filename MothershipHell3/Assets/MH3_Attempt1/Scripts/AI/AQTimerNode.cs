using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName ="Timer", menuName ="AI/Timer",order = 1)]
public class AQTimerNode : AQNode {
    // success when it ticks, otherwise false. 

    [SerializeField] float activateAtTime;
    [SerializeField] float rate = 0.1f;

    public override AQResult Execute()
    {
        if (Time.time >= activateAtTime)
        {
            activateAtTime = Time.time + rate;
            return AQResult.Success;
        }
        return AQResult.Fail;
    }
}
