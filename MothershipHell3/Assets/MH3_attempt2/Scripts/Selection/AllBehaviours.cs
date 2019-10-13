using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// References to all targets.
/// </summary>
public class AllBehaviours : MonoBehaviour {

    List<AITTarget> _gameBehaviours;
    
    // API properties
    public List<AITTarget> GameBehaviours { get => _gameBehaviours; }

    private void Awake()
    {
        _gameBehaviours = new List<AITTarget>();
    }

    public void RegisterAsBehaviour(AITTarget obj)
    {
        _gameBehaviours.Add(obj);
    }
}
