using System;
using UnityEngine;

[Serializable]
public class ObjectReference {
    // use to reference scene objects.

    [SerializeField] bool useConstant;
    [SerializeField] GameObject constant;
    [SerializeField] ObjectValue number;

    public ObjectReference(ObjectValue value)
    {
        this.number = value;
    }

    public ObjectReference(GameObject constant)
    {
        this.constant = constant;
    }

    public GameObject Value {
        get => useConstant ? constant : (number!= null ? number.value : constant);
        set {
            if (useConstant)
                constant = value;
            else if (number != null)
                number.value = value;
            else
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Debug.LogError("Reference not found, set to constant instead. "+ t.ToString());
                constant = value;
            }
        }
    }

    // destroy reference to object.
    internal void ClearMemory()
    {
        ScriptableObject.DestroyImmediate(number);
    }
}
