using UnityEngine;

[System.Serializable]
public abstract class AQListNode : AQNode {
    [SerializeField] public AQNode[] conditions;

}