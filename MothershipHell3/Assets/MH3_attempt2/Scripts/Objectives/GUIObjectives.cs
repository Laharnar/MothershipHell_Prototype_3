using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIObjectives : MonoBehaviour
{
    [SerializeField] FadeOutColor onGameDoneFadeout;
    [SerializeField] List<FadeOutColor> objectivesDone = new List<FadeOutColor>();

    private void Start()
    {
        // dev test
        Invoke("test", 3);
    }
    void test()
    {
        UIForCompleteObjective(0);

    }

    public void UIForCompleteMission()
    {
        OnDone();
    }

    public void UIForCompleteObjective(int i)
    {
        Debug.Log("fade ut");
        // .
        objectivesDone[i].FadeOut();
    }

    public void UIForFailedObjective(int i)
    {
        Debug.Log("Failed objective");
    }

    public void RegisterObjective()
    {
        // .
    }

    public void UnRegisterObjective()
    {
        // .
    }

    IEnumerator OnDone()
    {
        while (true)
        {
            yield return StartCoroutine(onGameDoneFadeout.cFadeOut());
        }
    }
}
