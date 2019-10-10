/* Created by: Marko Laharnar on 7.9.2019
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Joins multiple turrets and turret rotation into one simple high level logic.
/// It should be used as API for AI.
/// </summary>
public class TurretAPI : MonoBehaviour
{
    [SerializeField] bool isRunning = true;

    [SerializeField] bool isRoot = false;
    [SerializeField] FiringBattery[] guns;
    [SerializeField] RotatingTurretTop[] rotatingParts;

    // behaviour for each turret.
    [SerializeField] AbandomPriority[] eachTurretTrackingType;

    [SerializeField] TargetPriority useForTargetQue = TargetPriority.LastInQue;
    [SerializeField] ScanningPriority whenToRepeatScanning = ScanningPriority.OnRotationDone;


    Queue<ReportedEvent> pastEvents = new Queue<ReportedEvent>();
    Queue<FireCommand> fireToExecute = new Queue<FireCommand>();

    List<TrackTargetEvent> targetsToActUpon = new List<TrackTargetEvent>();
    public int bulletsFired { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (!isRoot)
        {
            transform.parent.GetComponentsInChildren<FiringBattery>();
            transform.parent.GetComponentsInChildren<RotatingTurretTop>();
        }
        StartCoroutine(MultiGunFireHandler());
        StartCoroutine(MultiGunRotationHandler());
    }

    private IEnumerator MultiGunRotationHandler()
    {
        yield return null;
        yield return null;
        while (true)
        {
            yield return null;
            if (!isRunning) continue;
            if (targetsToActUpon.Count == 0) continue;
            if (rotatingParts.Length == 0) continue;

            // todo: include priority.
            TrackTargetEvent target = targetsToActUpon[targetsToActUpon.Count-1];
            Debug.Log("assigned target to " +transform.root + target.target+target.context);
            AQAccess.Ins.SetData(transform.root.GetComponentInChildren<ShipAI>(), "Target", target.target);

            // last one is assumed to be root 
            // todo: make it safer.
            RotatingTurretTop rotatingPart = rotatingParts[rotatingParts.Length-1];

            foreach (var turretTracking in eachTurretTrackingType)
            {
                switch (turretTracking)
                {
                    case AbandomPriority.AimedOnce:
                        RotateToTarget(rotatingPart, target);
                        break;
                    case AbandomPriority.OnDeath:
                        break;
                    case AbandomPriority.OnLoseRange:
                        break;
                    case AbandomPriority.OnInRange:
                        break;
                    case AbandomPriority.AfterNextShotCommand:
                        break;
                    default:
                        break;
                }
            }
            targetsToActUpon.RemoveAt(targetsToActUpon.Count - 1);
        }
    }

    private void RotateToTarget(RotatingTurretTop rotatingPart, TrackTargetEvent target)
    {
        if (transform.root == target.target.root)
        {
            Debug.LogError("Aiming at itself", this);
        }

        switch (target.idleMoving)
        {
            case TargetType.Moving:

                rotatingPart.TurnToPoint(target.target.position);
                break;
            case TargetType.NonMoving:
                rotatingPart.TurnToPoint(target.target.position);
                break;
            default:
                break;
        }
    }

    private IEnumerator MultiGunFireHandler()
    {
        while (true)
        {
            yield return null;
            if (!isRunning) continue;

            if (fireToExecute.Count > 0)
            {
                FireCommand command = fireToExecute.Dequeue();
                switch (command)
                {
                    case FireCommand.FireOneReady:
                        for (int i = 0; i < guns.Length; i++)
                        {
                            if (guns[i].IsReady)
                            {
                                guns[i].holdFire = false;
                                yield return null;
                                guns[i].holdFire = true;
                                break;
                            }
                        }
                        bulletsFired++;
                        break;
                    case FireCommand.FireAll:
                        for (int i = 0; i < guns.Length; i++)
                        {
                            if (guns[i].IsReady)
                            {
                                guns[i].holdFire = false;
                            }
                        }
                        bulletsFired+=guns.Length;
                        yield return null;
                        for (int i = 0; i < guns.Length; i++)
                        {
                            guns[i].holdFire = true;
                        }
                        break;
                    case FireCommand.WaitAllRotationsReady:
                        for (int i = 0; i < rotatingParts.Length; i++)
                        {
                            while (!rotatingParts[i].IsIdle)
                            {
                                yield return null;
                            }
                        }
                        break;
                    case FireCommand.WaitOneRotationReady:
                        bool oneRotationDone = false;
                        while (!oneRotationDone)
                        {
                            for (int i = 0; i < rotatingParts.Length; i++)
                            {
                                if (rotatingParts[i].IsIdle)
                                {
                                    oneRotationDone = true;
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        Debug.Log("Skipped fire command: "+command);
                        break;
                }
            }
            pastEvents.Enqueue(ReportedEvent.FireCommandComplete);
        }
    }

    public void Fire(FireCommand type)
    {
        fireToExecute.Enqueue(type);
    }

    public void AssignTarget(Transform target, TargetType idleMoving, string context)
    {
        targetsToActUpon.Add(new TrackTargetEvent(target, idleMoving, context));
    }

    public void TurnTowardsEmptySpace(Vector2 pt)
    {
        GameObject temp = new GameObject("_-TempTarget");
        temp.transform.position = pt;

        targetsToActUpon.Add(new TrackTargetEvent(temp.transform, TargetType.NonMoving, "'turn towards empty space'"));
    }

    public Quaternion GunRotation(int gunId)
    {
        if (gunId >= rotatingParts.Length)
        {
            Debug.LogError("Index out of range: Number of guns is less than "+(gunId+1));
            return new Quaternion();
        }
        return rotatingParts[gunId].transform.localRotation;
    }
}

public struct TrackTargetEvent {
    public Transform target;
    public TargetType idleMoving;
    public string context;

    public TrackTargetEvent(Transform target, TargetType idleMoving, string context)
    {
        this.target = target;
        this.idleMoving = idleMoving;
        this.context = context;
    }
}

public enum TargetType {
    Moving,
    NonMoving
}

enum TargetPriority {
    FirstInQue,
    LastInQue,
    Closest,
    Furthest,
    AverageDistance,
    Oldest,
    Youngest,
    Random
}

public enum AbandomPriority {
    AimedOnce,
    OnDeath,
    OnLoseRange,
    OnInRange,
    AfterNextShotCommand,
}

enum ScanningPriority {
    OnCurrentLost,
    OnTimer,
    OnRotationDone,
    OnShotDone
}

enum ReportedEvent {
    TargetDestroyed,
    TargetAbandoned,
    TargetLost,
    FireCommandComplete,
    RotationCommandComplete,
}

public enum FireCommand {
    FireUntilInterruption,
    Interrupt,
    FireOneReady,
    FireAll,
    WaitOneRotationReady,
    WaitAllRotationsReady

}