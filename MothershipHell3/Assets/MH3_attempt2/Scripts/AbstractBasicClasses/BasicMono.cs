using UnityEngine;

public abstract class BasicMono : MonoBehaviour {

    public Vector2 LocalPos { get => (Vector2)transform.localPosition; set => transform.localPosition = value; }
    public Vector2 WorldPos { get => (Vector2)transform.position; set => transform.position = value; }

    [SerializeField] bool _isLocked;
    protected bool IsLocked { get => _isLocked;
        set {
            LastIsLocked = _isLocked;
            _isLocked = value;
        }
    }
    protected bool LastIsLocked;
    [SerializeField] protected bool IsInit;

    protected virtual void Initialize() {

        IsInit = true;
        LastIsLocked = !_isLocked;
        LoadComponents();
        Preloader();
    }

    protected virtual void OnIsLockedChange(bool newState) { /*. */ }
    protected abstract void OnIsUnlockedUpdate();
    protected abstract void OnIsLockedUpdate();

    protected abstract void Preloader();// allows any script to load this object.
    protected virtual void LoadComponents() {
        // .
    }

    // don't override
    protected void Start()
    {
        Initialize();
    }

    // don't override
    protected void Update()
    {
        if (!IsInit) return;
        if (!IsLocked)
        {
            OnIsUnlockedUpdate();
        }
        else
        {
            OnIsLockedUpdate();
        }
    }

    protected virtual void DestroyObj()
    {
        GameObject.Destroy(gameObject);
    }

}
