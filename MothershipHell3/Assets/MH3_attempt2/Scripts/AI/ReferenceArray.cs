using System;
using UnityEngine;

[CreateAssetMenu]
public class ReferenceArray : ScriptableObject {

    [SerializeField] FloatReference[] refs;

    internal void Call(Func<FloatReference, float> callback, FloatReference setTo)
    {
        for (int i = 0; i < refs.Length; i++)
        {
            refs[i].Value = callback(setTo);
        }
        callback(setTo);
    }
}
