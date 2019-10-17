using UnityEngine;
using UnityEngine.Events;

public class GameEventListener:MonoBehaviour {

    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Debug.Log("Registred event "+ Event.name, this);
        Event.RegisterListener(this);
    }
    private void OnDisable()
    {
        Debug.Log("Unregistred event "+ Event.name, this);
        Event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        Debug.Log("Event raised "+ Event.name, this);
        Response.Invoke();
    }
}
