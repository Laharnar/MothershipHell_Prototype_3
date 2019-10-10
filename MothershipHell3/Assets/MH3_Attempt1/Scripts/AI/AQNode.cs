using UnityEngine;
/// <summary>
/// Base for AQ node system. AQ: 2 closest characters. BT(behaviour trees) is harder to write
/// </summary>
public abstract class AQNode:ScriptableObject {
    public string context;
    public abstract AQResult Execute();
}
