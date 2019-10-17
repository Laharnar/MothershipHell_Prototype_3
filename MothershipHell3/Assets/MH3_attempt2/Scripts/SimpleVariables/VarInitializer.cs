using UnityEngine;

public class VarInitializer:MonoBehaviour {

    [SerializeField] FloatReference setTo;
    [SerializeField] FloatReferenceArray refs;

    private void Start()
    {
        refs.Call(SetValue, setTo);
    }

    float SetValue(FloatReference varRef)
    {
        return varRef.Value;
    }
}
