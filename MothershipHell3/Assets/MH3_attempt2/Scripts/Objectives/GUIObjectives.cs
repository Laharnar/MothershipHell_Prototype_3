using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIObjectives : BasicMono
{
    // ui results as objectives get solved or added.
    [SerializeField] FadeOutColor onGameDoneFadeout;
    [SerializeField] List<FadeOutColor> objectivesDone = new List<FadeOutColor>();

    // setup data
    [SerializeField] ObjectiveDestroyEnemies[] objectives;
    
    // instance data
    [Tooltip("current objectives")]
    [SerializeField] List<Objective> existingObjectives;

    protected override void Preloader()
    {
        base.Preloader();
        // init objectives.
        existingObjectives = new List<Objective>();
        for (int i = 0; i < objectives.Length; i++)
        {
            existingObjectives.Add( 
                new Objective() { completed = false,
                    data = objectives[i] });
        }
    }

    protected override void OnIsUnlockedUpdate()
    {
        base.OnIsUnlockedUpdate();
        bool allDone = true;
        List<AITTarget> targets = this.GetUniqueClass<AITGlobalTracking>().Trackable;
        for (int i = 0; i < existingObjectives.Count; i++)
        {
            
            if (!existingObjectives[i].completed && existingObjectives[i].Check(targets))
            {
                existingObjectives[i].completed = true;
                UIForCompleteObjective(i);
            }
        }

        allDone = true;
        for (int i = 0; i < objectives.Length; i++)
        {
            if (!existingObjectives[i].completed)
            {
                allDone = false;
                break;
            }
        }
        if (allDone && !IsLocked)
        {
            Invoke("UIForCompleteMission", 3);
            IsLocked = true;
        }
    }

    public void UIForCompleteMission()
    {
        Debug.Log("Mission complete.");
        StartCoroutine(OnDone());
    }

    public void UIForCompleteObjective(int i)
    {
        Debug.Log("fade ut");
        // .
        objectivesDone[i].FadeOut();
    }

    public void UIForFailedObjective(int i)
    {
        Debug.Log("Failed objective");
    }

    public void RegisterObjective()
    {
        // .
    }

    public void UnRegisterObjective()
    {
        // .
    }

    IEnumerator OnDone()
    {
        yield return StartCoroutine(onGameDoneFadeout.cFadeOut());
    }
}
