using UnityEngine;

public class LoseHpCollision:MonoBehaviour {

    Stats hp; // assign somewhere in one of parent.
    OnCollision collision;

    private void Awake()
    {
        collision = GetComponent<OnCollision>();
        hp = GetComponentInParent<Stats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision otherCollision = other.GetComponent<OnCollision>();
        if (OnCollision.DifferentAlliance(collision, otherCollision))
        {
            otherCollision.SendDamage(hp);
        }
    }
}
