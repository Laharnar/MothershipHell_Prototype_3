﻿using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName ="For", menuName ="AI/FOR",order = 1)]
public class AQForNode : AQDecoratorNode {
    [SerializeField] int repeatTimes;

    public override AQResult Execute()
    {
        for (int i = 0; i < repeatTimes; i++)
        {
            child.Execute();
        }
        return AQResult.Undefined;
    }
}
