using System;
using UnityEngine;

[Serializable]
public class FloatReference {
    // use to reference scene objects.

    [SerializeField] bool useConstant;
    [SerializeField] float constant;
    [SerializeField] FloatVariable number;

    public float Value {
        get {
            if (number == null && !useConstant)
            {
                Debug.LogError("Float reference is null. Defaulting to constant to avoid Null error.");
                return constant;
            }
            return useConstant ? constant : number.value;
        }
        set {
            if (useConstant)
                constant = value;
            else if (number != null)
                number.value = value;
            else
            {
                Debug.LogError("Float reference is null. Setting constant to avoid Null error.");
                constant = value;
            }
        }
    }

}
