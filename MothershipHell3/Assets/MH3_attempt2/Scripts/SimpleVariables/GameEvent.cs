using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject{

    private List<GameEventListener> listeners =
        new List<GameEventListener>();

    public virtual void Raise()
    {
        Debug.Log("Raised GameEvent."+name);
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i] == null)
            {
                listeners.RemoveAt(i);
                i--;
                Debug.Log("Removed null value from GameEvent listener.", this);
            }else
                listeners[i].OnEventRaised();
        }
    }

    public void ClearListeners()
    {
        listeners.Clear();
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    private void Awake()
    {
        Debug.Log("awake called ",this);
        ClearListeners();
    }
}
