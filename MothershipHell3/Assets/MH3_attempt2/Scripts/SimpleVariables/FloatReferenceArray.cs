using System;
using UnityEngine;

[CreateAssetMenu]
public class FloatReferenceArray : ScriptableObject {

    [SerializeField] FloatReference[] refs;

    public void Call(Func<FloatReference, float> callback, FloatReference setTo)
    {
        for (int i = 0; i < refs.Length; i++)
        {
            refs[i].Value = callback(setTo);
        }
        callback(setTo);
    }
}
