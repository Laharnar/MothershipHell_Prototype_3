using UnityEngine;

public class ObjectRegister : MonoBehaviour, IPooling {

    [SerializeField] bool used = false;

    int idInArray = 0;
    [SerializeField] ObjectReferenceArray registerOnStart;
    [SerializeField] ObjectReferenceArray unregisterOnDestroy;
    [SerializeField] ObjectReferenceArray againRegisterOnPoolCreated;
    [SerializeField] ObjectReferenceArray unregisterOnPoolDestroy;

    [HideInInspector]public string PoolingGroupTag { get; }

    private void Start()
    {
        if (!used) return;
        if (registerOnStart)
            registerOnStart.Register(gameObject);
        else Debug.Log("Missing register on start.", this);
    }

    private void OnDestroy()
    {
        if (!used) return;
        if (unregisterOnDestroy)
            unregisterOnDestroy.Unregister(gameObject);
        else Debug.Log("Missing Unregister on OnDestroy.", this);
    }

    public void OnPooledCreated()
    {
        if (!used) return;
        if (againRegisterOnPoolCreated)
            againRegisterOnPoolCreated.Register(gameObject);
        else Debug.Log("Missing Register on OnPooledCreated.", this);
    }

    public void OnPooledDestroyed()
    {
        if (!used) return;
        if (unregisterOnPoolDestroy)
            unregisterOnPoolDestroy.Unregister(gameObject);
        else Debug.Log("Missing Unregister on OnPooledDestroyed.", this);
    }
}
