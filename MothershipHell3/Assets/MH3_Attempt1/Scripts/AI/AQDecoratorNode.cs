using UnityEngine;

public abstract class AQDecoratorNode : AQNode {
    [SerializeField] public AQNode child;
}