using UnityEngine;

public abstract class STANDSelectableMono : BasicMono, ISelectable {

    protected bool IsSelected = false;

    protected override void Preloader()
    {
        //Debug.Log("Preload selectable.", this);
        if (this.TryGetUniqueClass<DragSelection>())
        {
            this.LastResult<DragSelection>().RegisterAsSelectable(this);
        }
    }

    public virtual void OnSelected()
    {
        IsSelected = true;
    }

    public virtual void OnDeselected()
    {
        IsSelected = false;
    }
}
