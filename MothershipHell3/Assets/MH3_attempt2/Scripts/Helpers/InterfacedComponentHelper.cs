using System.Collections.Generic;
using UnityEngine;

public static class InterfacedComponentHelper {

    static Dictionary<AIBFollow, IAITControllable> cache = new Dictionary<AIBFollow, IAITControllable>();

    public static IAITControllable GetByBehaviour(this AIBFollow aib)
    {
        switch (aib.Behaviour)
        {
            case PossibleInterfaceClasses.IAITControllable:
                if (cache.ContainsKey(aib))
                    return cache[aib];
                cache.Add(aib, aib.GetComponent<IAITControllable>());
                return cache[aib];
            default:
                Debug.LogError("Unhandled enum.");
                break;
        }
        return null;
    }
}
