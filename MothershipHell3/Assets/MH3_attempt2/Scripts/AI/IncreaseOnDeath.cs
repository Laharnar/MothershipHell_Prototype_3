using UnityEngine;

/// <summary>
/// Increases value by given value. Supports pooling.
/// </summary>
public class IncreaseOnDeath:MonoBehaviour, IPooling {
    [SerializeField] FloatReference add;
    [SerializeField] FloatReference change;
    [SerializeField] IntReference iadd;
    [SerializeField] IntReference ichange;

    // prevents double triggers in case of destroy + pool was called here.
    bool triggered = false;

    // remove from IPooling
    /// <summary>
    /// Don't use this here.
    /// </summary>
    public string PoolingGroupTag { get; }

    public void OnPooledCreated()
    {
        // .
        triggered = false;
    }

    public void OnPooledDestroyed()
    {
        if (triggered) return;

        change.Value += add.Value;
        ichange.Value += iadd.Value;
        triggered = true;
    }

    private void OnDestroy()
    {
        if (triggered) return;
        triggered = true;
        change.Value += add.Value;
        ichange.Value += iadd.Value;
    }
}
