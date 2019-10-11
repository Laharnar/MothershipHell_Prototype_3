using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldenRatioScale : MonoBehaviour
{
    [MenuItem("GoldenRatio/Apply golden ratio from X to Y _u")]
    static void ApplyGoldenRatioBasedOnXscale()
    {
        Undo.RecordObject(Selection.activeTransform, "Rescale to golden ratio X to Y");
        Selection.activeTransform.localScale = new Vector3(Selection.activeTransform.localScale.x , Selection.activeTransform.localScale.x * 1.61803399f);
    }

    [MenuItem("GoldenRatio/Apply golden ratio from Y to X _i")]
    static void ApplyGoldenRatioBasedOnYscale()
    {
        Undo.RecordObject(Selection.activeTransform, "Rescale to golden ratio Y to X");
        Selection.activeTransform.localScale = new Vector3(Selection.activeTransform.localScale.y * 1.61803399f, Selection.activeTransform.localScale.y);
    }
    [MenuItem("Scaling/Set to 1 _o")]
    static void ApplyScaleOf1()
    {
        Undo.RecordObject(Selection.activeTransform, "Rescale to golden ratio Y to X");
        Selection.activeTransform.localScale = Vector3.one;
    }
}
