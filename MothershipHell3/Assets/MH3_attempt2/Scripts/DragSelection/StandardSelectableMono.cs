using UnityEngine;

public abstract class StandardSelectableMono : BasicMono, ISelectable {

    bool IsSelected;

    // don't override
    protected void Start()
    {
        Debug.Log("Start", this);
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

    protected override void Preloader()
    {
        Debug.Log("Preloagde", this);
        if (this.TryGetUniqueClass<DragSelection>())
        {
            this.LastResult<DragSelection>().RegisterAsSelectable(this);
        }
    }


    public void OnSelected()
    {
        IsSelected = true;
    }

    public void OnDeselected()
    {
        IsSelected = false;
    }
}
