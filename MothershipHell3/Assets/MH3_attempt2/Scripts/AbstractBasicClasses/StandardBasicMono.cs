public abstract class StandardBasicMono: BasicMono {

    // don't override
    protected void Start()
    {
        Initialize();
    }

    // don't override
    protected void Update()
    {
        if (IsInit) return;
        if (!IsLocked)
        {
            OnIsUnlocked();
        }
        else
        {
            OnIsLocked();
        }
    }
}
