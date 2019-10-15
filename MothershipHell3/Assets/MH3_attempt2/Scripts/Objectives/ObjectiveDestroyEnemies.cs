using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveDestroy", menuName = "Objectives/DestroyEnemies", order = 1)]
public class ObjectiveDestroyEnemies:ScriptableObject {
    // objective to destroy all enemies currently on map.
    // relies on AITGlobalTracking.cs to get information

    // current implementation might not  be appropriate long term(because some could not be mission targets, but still targetable).


    public int destroyAlliance;

    internal bool Check(List<AITTarget> roster)
    {
        // true, when there isn't any target of certain alliance.
        for (int i = 0; i < roster.Count; i++)
        {
            if (roster[i].Alliance == destroyAlliance)
            {
                return false;
            }
        }
        return true;
    }

}
