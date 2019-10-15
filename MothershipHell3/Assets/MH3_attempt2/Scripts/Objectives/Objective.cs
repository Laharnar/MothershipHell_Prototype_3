using System;
using System.Collections.Generic;

[Serializable]
public class Objective {
    public ObjectiveDestroyEnemies data;
    public bool completed = false;

    internal bool Check(List<AITTarget> targets)
    {
        return data.Check(targets);
    }
}
