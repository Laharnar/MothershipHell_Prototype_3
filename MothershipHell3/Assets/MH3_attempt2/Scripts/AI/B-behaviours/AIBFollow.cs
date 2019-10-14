using UnityEngine;
 
public class AIBFollow:BasicMono {
    [SerializeField] AITTarget core;
    [SerializeField] PossibleInterfaceClasses _behaviour;

    public AIBFollowType targetType = AIBFollowType.AIDefined;

    Camera _cam;

    public enum AIBFollowType {
        AIDefined,
        PlayerMouse
    }

    public PossibleInterfaceClasses Behaviour {
        get => _behaviour;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _cam = Camera.main;
    }

    protected override void OnIsUnlockedUpdate()
    {
        base.OnIsUnlockedUpdate();
        if (targetType == AIBFollowType.AIDefined)
        {
            if (!core.HasTarget)
            {
                core.AutoSeekFirstEnemy();
            }
            else
            {
                IAITControllable aITControllable = this.GetByBehaviour();
                if (aITControllable != null)
                    aITControllable.AITAimMoveTo(core.ActiveTrackPos);
                else Debug.Log("No target to move to " + aITControllable + core);
            }
        }else if(targetType == AIBFollowType.PlayerMouse) {
            Vector2 pos = _cam.ScreenToWorldPoint(Input.mousePosition);

            // set to track mouse position
            if (core != null)
                core.AITMove(pos);
            else Debug.Log("No core assigned.");

            // update actual aiming
            IAITControllable aITControllable = this.GetByBehaviour();
            if (aITControllable != null)
            {
                aITControllable.AITAimMoveTo(core.ActiveTrackPos);
            }
            else Debug.Log("No target to move to " + aITControllable + core);
        }
    }
}
