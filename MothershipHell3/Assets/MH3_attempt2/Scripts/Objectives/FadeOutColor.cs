using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutColor : MonoBehaviour
{
    [SerializeField] Color fadeOutColor;
    [SerializeField] Color fadeInColor;

    [SerializeField] Image img;
    [SerializeField] TMPro.TextMeshProUGUI txt;

    [SerializeField] bool fadeInAtStart = false;

    Color c;

    private void Start()
    {
        if (fadeInAtStart)
            FadeIn();
    }

    public void FadeOut()
    {
        StartCoroutine(cFadeOut());
    }

    public void FadeIn()
    {
        StartCoroutine(cFadeIn());
    }

    internal IEnumerator cFadeOut()
    {
        Debug.Log("fade ut2");
        SetColor(fadeInColor);
        float t = 0f;
        while (t<1f)
        {
            t += Time.deltaTime;
            SetColor( Color.Lerp(fadeInColor, fadeOutColor, t));
            yield return null;
        }
    }

    internal IEnumerator cFadeIn()
    {
        SetColor(fadeOutColor);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            SetColor( Color.Lerp(fadeOutColor, fadeInColor, t));
            yield return null;
        }
    }

    void SetColor(Color col)
    {
        if (txt != null)
        {
            txt.color = col;
        }
        if (img != null)
        {
            img.color = col;
        }
    }
}
