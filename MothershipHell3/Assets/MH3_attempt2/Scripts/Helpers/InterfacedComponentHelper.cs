using UnityEngine;

public static class InterfacedComponentHelper {
    public static IAITControllable GetByBehaviour(this AIBFollow aib)
    {
        switch (aib.Behaviour)
        {
            case PossibleInterfaceClasses.IAITControllable:
                return aib.GetComponent<IAITControllable>();
                break;
            default:
                Debug.LogError("Unhandled enum.");
                break;
        }
        return null;
    }
}
