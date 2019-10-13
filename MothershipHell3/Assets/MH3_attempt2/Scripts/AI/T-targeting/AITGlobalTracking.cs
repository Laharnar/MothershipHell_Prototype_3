using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITGlobalTracking:MonoBehaviour {

    public List<AITTarget> Trackable { get; private set; }
    /// <summary>
    /// Represents filter, which trackable targets should be used, based on last criteria.
    /// </summary>
    public bool[] LastMask { get; private set; }

    /// <summary>
    /// Just to display data in inspector.
    /// </summary>
    [SerializeField] GameObject[] dev_Selection;

    private void Awake()
    {
        Trackable = new List<AITTarget>();
    }

    /// <summary>
    /// Should be called on targets that you want to be trackable.
    /// </summary>
    /// <param name="target"></param>
    public void RegisterAITTarget(AITTarget target)
    {
        if (target == null)
        {
            Debug.LogError("RegisterAIT:Passed AITTargets value is null. Aborting.");
        }
        else
        {
            Debug.Log("Register targetabl " + target, target);
            Trackable.Add(target);
        }
    }

    /// <summary>
    /// Should be called on targets that were trackable.
    /// </summary>
    /// <param name="target">It's okay to pass invalid value. Null might not work for debug.</param>
    public void UnRegisterAITTarget(AITTarget target)
    {
        Trackable.Remove(target);

        dev_Selection = new GameObject[Trackable.Count];
        for (int i = 0; i < Trackable.Count; i++)
        {
            dev_Selection[i] = Trackable[i].SelectionSource.gameObject;
        }
        Debug.Log("UnRegister targetable " + target, target);
    }

    /// <summary>
    /// Returns indexed 
    /// </summary>
    /// <param name="index">gets index of VALID, not of SELECTION.</param>
    /// <returns></returns>
    public AITTarget GetByIdUnderMask(int index)
    {
        int count = 0;
        for (int i = 0; i < LastMask.Length; i++)
        {
            if (LastMask[i])
            {
                if (count == index)
                {
                    return Trackable[i];
                }
                count++;
            }
            
        }
        Debug.Log("AITSelection - get - Index is out of range. nothing to select. id: "+ index + " range "+LastMask.Length);
        return null;
    }

    /// <summary>
    /// Sets 
    /// </summary>
    /// <param name="pickAlliance"></param>
    public void LoadAlliesAsTracked(int pickAlliance)
    {
        List<AITTarget> original = Trackable;
        int size = original.Count;
        LastMask = new bool[size];
        dev_Selection = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            LastMask[i] = false;
            
            dev_Selection[i] = original[i].SelectionSource.gameObject;
            AITTarget source = original[i].SelectionSource2 as AITTarget;
            if (source != null && source.Alliance == pickAlliance)
                LastMask[i] = true;
        }
    }
    public void LoadEnemiesAsTracked(int pickAlliance)
    {
        List<AITTarget> original = Trackable;
        int size = original.Count;
        LastMask = new bool[size];
        dev_Selection = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            //Selection[i] = original[i];
            LastMask[i] = false;
            if (original[i] == null)
            {
                Debug.Log("AITSelection.Value is null at "+i);
                continue;
            }

            dev_Selection[i] = original[i].SelectionSource.gameObject;
            AITTarget source = original[i].SelectionSource2 as AITTarget;
            if (source != null && source.Alliance != pickAlliance)
                LastMask[i] = true;
        }
    }
}
