using UnityEngine;

public abstract class STANDPhysicsMono : BasicMono {

    protected Rigidbody2D rig;
    [SerializeField] protected bool updatePhysics = true;


    protected void FixedUpdate()
    {
        if(updatePhysics)
            OnPhysicsUpdate();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(rig == null) rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerIn2D(collision);
    }

    protected abstract void OnPhysicsUpdate();
    protected abstract void OnTriggerIn2D(Collider2D col);

}
