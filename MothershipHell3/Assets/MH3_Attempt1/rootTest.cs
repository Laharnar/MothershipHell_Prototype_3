using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootTest : MonoBehaviour {

    public Transform otherRoot;
    public Animator otherAnim;

    private void Start()
    {
        StartCoroutine(AnimPlay());
    }

    private IEnumerator AnimPlay()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {

            Vector2 oldPos = otherRoot.transform.position;
            PositionAnimationToThis();

            otherAnim.SetTrigger("Test");
            yield return new WaitForSeconds(1);

            RootThisToAnimation();

            otherAnim.SetTrigger("Test2");
            yield return new WaitForSeconds(3);
            DeparentThisFromAnimation();

            // move animation back.
            otherRoot.transform.position = oldPos;
        }
    }

    public void RootThisToAnimation()
    {
        transform.parent = otherAnim.transform;
    }

    public void DeparentThisFromAnimation()
    {
        transform.parent = null;
    }

    public void PositionAnimationToThis()
    {
        otherRoot.transform.position = transform.position;
    }

}
