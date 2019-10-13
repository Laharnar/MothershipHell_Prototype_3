using UnityEngine;
 
public class AIBFollow:BasicMono {
    [SerializeField] AITTarget core;
    [SerializeField] PossibleInterfaceClasses _behaviour;

    public PossibleInterfaceClasses Behaviour {
        get => _behaviour;
    }

    protected override void OnIsUnlockedUpdate()
    {
        base.OnIsUnlockedUpdate();
        if (!core.HasTarget)
        {
            core.AutoSeekFirstEnemy();
        }
        else
        {
            IAITControllable aITControllable = this.GetByBehaviour();
            if (aITControllable != null)
                aITControllable.AITMoveTo(core.ActiveTrackPos);
            else Debug.Log("No target to move to "+ aITControllable+core);
        }
    }
}
