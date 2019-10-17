using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu, Obsolete("Check out ObjectReferenceArray")]
public class ObjectArray : ScriptableObject {

    [SerializeField] List<GameObject> refs = new List<GameObject>();
    

    internal bool CheckConditionAny(Func<GameObject, bool> callback)
    {
        for (int i = 0; i < refs.Count; i++)
        {
            if (callback(refs[i]))
                return true;
        }
        return false;
    }

    internal bool CheckConditionAll(Func<GameObject, bool> callback)
    {
        for (int i = 0; i < refs.Count; i++)
        {
            if (!callback(refs[i]))
                return false;
        }
        return true;
    }

    internal void Register(GameObject gameObject)
    {
        refs.Add(gameObject);
    }

    internal void Unregister(GameObject gameObject)
    {
        refs.Remove(gameObject);
    }
}
