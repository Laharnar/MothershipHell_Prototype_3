using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI:MonoBehaviour, IAQAccessible {

    public AQNode root;

    public Transform Obj { get => transform; }
    [SerializeField] Transform actualShipRoot;
    public Transform ActualShipRoot { get => actualShipRoot; }

    Stack<object> gizmoStack = new Stack<object>();

    private void Start()
    {
        GlobalAQHelper.SimpleInitShip(this);
    }

    private void Update()
    {
        GlobalAQHelper.AssignActiveSource(this);
        root.Execute();
    }

    private void OnDrawGizmos()
    {
        while (gizmoStack.Count > 0)
        {
            object posRange = gizmoStack.Pop();
            object[] posRangeArr = (object[])posRange;
            Vector3 pos = (Vector3)posRangeArr[0];
            float range = (float)posRangeArr[1];
            Gizmos.DrawSphere(pos, range);
        }
    }

    internal void DrawGizmoSphere(Vector3 position, float range)
    {
        gizmoStack.Push(new object[] { position, range });
    }
}
