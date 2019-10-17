
using UnityEngine;
using UnityEngine.Events;

public class GameEvents:MonoBehaviour, IPooling {
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onDestroy;
    [SerializeField] UnityEvent onPool;
    [SerializeField] UnityEvent onUnpool;

    [HideInInspector]public string PoolingGroupTag { get; }

    private void Start()
    {
        if (onStart!= null)
            onStart.Invoke();
    }

    private void OnDestroy()
    {
        if (onDestroy != null)
            onDestroy.Invoke();
    }

    public void OnPooledCreated()
    {
        if (onPool != null)
            onPool.Invoke();
    }

    public void OnPooledDestroyed()
    {
        if (onUnpool != null)
            onUnpool.Invoke();
    }
}
