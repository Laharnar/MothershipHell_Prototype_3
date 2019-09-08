using System;

public class GlobalAQHelper {

    public static void AssignActiveSource(IAQAccessible source)
    {
        AQAccess.ActiveSource = source;
    }

    internal static void SimpleInitShip(ShipAI shipAI)
    {
        AQAccess.Register(shipAI);
        AQAccess.Ins.SetData(shipAI, "Root", shipAI.ActualShipRoot);
        AQAccess.Ins.SetData(shipAI, "Source", shipAI);

        ShipAPI sapi = shipAI.GetComponentInChildren<ShipAPI>();
        TurretAPI tapi = shipAI.GetComponentInChildren<TurretAPI>();

        AQAccess.Ins.SetData(shipAI, "TurretControl", tapi);
        AQAccess.Ins.SetData(shipAI, "TurretControl", tapi);

    }

}