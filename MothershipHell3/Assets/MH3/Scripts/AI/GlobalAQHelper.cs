using System;
using UnityEngine;

public class GlobalAQHelper {

    public static void AssignActiveSource(IAQAccessible source)
    {
        AQAccess.ActiveSource = source;
    }

    internal static void SimpleInitShip(ShipAI shipAI)
    {
        AQAccess.Register(shipAI);
        if (shipAI == null) {
            Debug.Log("ShipAi function parameter is null -> Aborting.");
            return;
        }
        ShipAPI sapi = shipAI.GetComponentInChildren<ShipAPI>();
        TurretAPI tapi = sapi.TurretControl;

        if (shipAI.ActualShipRoot == null) Debug.Log("Missing ship root. np.");
        if (sapi == null) Debug.Log("Missing ship API. np.");
        if (sapi == null) Debug.Log("Missing turret API. np.");

        AQAccess.Ins.SetData(shipAI, "Source", shipAI);
        AQAccess.Ins.SetData(shipAI, "Root", shipAI.ActualShipRoot);
        AQAccess.Ins.SetData(shipAI, "TurretControl", tapi);

    }

}