using UnityEngine;

public abstract class BasicMono : MonoBehaviour {

    public Vector2 LocalPos { get => (Vector2)transform.localPosition; set => transform.localPosition = value; }
    public Vector2 WorldPos { get => (Vector2)transform.position; set => transform.position = value; }

    protected bool IsLocked;
    protected bool IsInit;

    protected virtual void Initialize() {
        Debug.Log("Init", this);
        IsInit = true;
        Preloader();
    }

    protected abstract void OnIsUnlocked();
    protected abstract void OnIsLocked();

    protected abstract void Preloader();// allows any script to load this object.

}
