using UnityEngine;

public abstract class STANDSelectableMono : BasicMono, ISelectable {

    // instance data
    [SerializeField] protected bool canPlayerSelectIt;
    protected bool isSelected = false;

    public BasicMono SelectionSource { get => this; }
    public STANDSelectableMono SelectionSource2 { get => this; }

    public bool CanPlayerSelectIt { get => canPlayerSelectIt; }
    public bool IsSelected { get => isSelected; }

    protected override void Preloader()
    {
        RegisterToDrag();
    }

    public virtual void OnSelected()
    {
        isSelected = true;
    }

    public virtual void OnDeselected()
    {
        isSelected = false;
    }

    protected override void DestroyObj()
    {
        UnRegisterFromDrag();
        base.DestroyObj();
    }

    protected void UnRegisterFromDrag()
    {
        if (CanPlayerSelectIt)
        {
            if (this.TryGetUniqueClass<DragSelection>())
            {
                this.LastResult<DragSelection>().UnRegisterAsSelectable(this);
            }
        }
    }

    protected void RegisterToDrag()
    {
        if (CanPlayerSelectIt)
        {
            if (this.TryGetUniqueClass<DragSelection>())
            {
                this.LastResult<DragSelection>().RegisterAsSelectable(this);
            }
        }
    }
}
