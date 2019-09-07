﻿using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName ="If", menuName ="AI/IF",order = 1)]
public class AQOrConditionNode : AQNode {
    [SerializeField] AQNode[] conditions;

    public override AQResult Execute()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].Execute() == AQResult.Success)
            {
                return AQResult.Success;
            }
        }
        return AQResult.Fail;
    }
}
