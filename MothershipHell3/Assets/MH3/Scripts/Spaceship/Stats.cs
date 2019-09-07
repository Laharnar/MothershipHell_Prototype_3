using System;
using UnityEngine;

public class Stats:MonoBehaviour, IAccessAlliance {

    [SerializeField] int maxHealth;
    [SerializeField] int curHealth = -1;
    [SerializeField] GameObject destroyOnZeroHp;
    [SerializeField] uint alliance;

    public int MaxHealth { get => maxHealth; }
    public int CurHealth { get => curHealth; }

    private void Awake()
    {
        curHealth = maxHealth;
        AllianceInfoHelper.InitChildrenToThis(this, alliance);
    }

    public void ReduceHp(int amount)
    {
        curHealth -= amount;

        if (curHealth <= 0)
        {
            Destroy(destroyOnZeroHp);
        }
    }

    public void SetAllianceId(uint id)
    {
        alliance = id;
    }
}
