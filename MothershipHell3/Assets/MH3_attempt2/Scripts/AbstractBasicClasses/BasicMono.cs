using UnityEngine;

public abstract class BasicMono : MonoBehaviour {

    public Vector2 LocalPos { get => (Vector2)transform.localPosition; set => transform.localPosition = value; }
    public Vector2 WorldPos { get => (Vector2)transform.position; set => transform.position = value; }

    [SerializeField] bool _isLocked;
    protected bool IsLocked { get => _isLocked;
        set {
            if (_isLocked != value)
            {
                _isLocked = value;
                BasicMono[] monos = GetComponentsInChildren<BasicMono>();
                for (int i = 0; i < monos.Length; i++)
                {
                    monos[i]._isLocked = value;
                    monos[i].OnIsLockedChange(_isLocked);
                }
            }
        }
    }
    protected bool LastIsLocked;
    [SerializeField] protected bool IsInit;

    void Initialize() {

        IsInit = true;
        LoadComponents();
        Preloader();
    }

    protected virtual void OnIsUnlockedUpdate() { }
    protected virtual void OnIsLockedUpdate() { }

    // get's triggered when locked/unlocked changes. not in start.
    protected virtual void OnIsLockedChange(bool isLocked) { /*. */ }

    // second phase in Initialization.
    protected virtual void Preloader() { }// allows any script to load this object.
    // separate phase for components in Initialization.
    protected virtual void LoadComponents() { }

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
        Debug.Log("Standard destroy on low level "+name, this);

        GameObject.Destroy(gameObject);
    }
}
