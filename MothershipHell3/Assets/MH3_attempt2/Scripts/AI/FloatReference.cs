using System;
using UnityEngine;

[Serializable]
public class FloatReference {

    [SerializeField] bool useConstant;
    [SerializeField] float constant;
    [SerializeField] FloatVariable number;

    public float Value {
        get => useConstant ? constant : number.value;
        set {
            if (useConstant)
                constant = value;
            else
                number.value = value;
        }
    }

}
