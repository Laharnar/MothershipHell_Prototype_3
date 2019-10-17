using System;
using UnityEngine;

[Serializable]
public class BoolReference {

    [SerializeField] bool useConstant;
    [SerializeField] bool constant;
    [SerializeField] BoolValue bvalue;

    public bool Value {
        get => useConstant ? constant : (bvalue != null ? bvalue.value : constant);
        set {
            if (useConstant)
                constant = value;
            else if (bvalue != null)
                bvalue.value = value;
            else
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Debug.LogError("Reference not found, set to constant instead. " + t.ToString());
                constant = value;
            }
        }
    }
}
