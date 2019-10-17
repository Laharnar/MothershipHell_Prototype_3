using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ObjectReferenceArray : ScriptableObject {

    [SerializeField] List<ObjectValue> refs = new List<ObjectValue>();

    internal bool CheckConditionAny(Func<ObjectValue, bool> callback)
    {
        for (int i = 0; i < refs.Count; i++)
        {
            if (callback(refs[i]))
                return true;
        }
        return false;
    }

    internal bool CheckConditionAll(Func<ObjectValue, bool> callback)
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
        ObjectValue obj = (ObjectValue) ScriptableObject.CreateInstance<ObjectValue>();
        obj.value = gameObject;
        refs.Add(obj);
    }

    internal void Unregister(GameObject gameObject)
    {
        for (int i = refs.Count - 1; i >= 0; i--)
        {
            if (refs[i].value == gameObject)
            {
                ScriptableObject.DestroyImmediate(refs[i]);
                refs.RemoveAt(i);
            }
        }
    }
}
