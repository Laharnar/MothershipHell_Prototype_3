using UnityEngine;

public class ShipAI:MonoBehaviour {
    public AQNode root;

    private void Update()
    {
        root.Execute();
    }
}
