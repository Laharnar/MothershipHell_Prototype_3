using UnityEngine;

public class Stats:MonoBehaviour {

    [SerializeField] int maxHealth;
    [SerializeField] int curHealth = -1;
    [SerializeField] GameObject destroyOnZeroHp;

    public int MaxHealth { get => maxHealth; }
    public int CurHealth { get => curHealth; }

    private void Awake()
    {
        curHealth = maxHealth;
    }

    public void ReduceHp(int amount)
    {
        curHealth -= amount;

        if (curHealth <= 0)
        {
            Destroy(destroyOnZeroHp);
        }
    }
}
