using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AQCreate : EditorWindow
{
    string path = "Prefabs/AI-basictree";
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    UnityEngine.Object[] assets;
    string[] assetFullPaths;
    int activeAsset = 0;

    const int skipLength = 28+14; // "Assets/MH3/Resources/Prefabs /Ai-basictree/";

    int maxRecursiveDepth = 30;
    int curRecursiveDepth = 0;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/AQCreate")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        AQCreate window = (AQCreate)EditorWindow.GetWindow(typeof(AQCreate));
        window.Show();


    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();

        LoadAssetsAndTheirPaths();

        // show asset drop down menu on left, which let's you pick assets.
        if (assets == null)
        {
            EditorGUILayout.LabelField("No assets are loaded.");
            return;
        }
        EditorGUILayout.LabelField("Avaliable assets - count:" + assets.Length);
        for (int i = 0; i < assets.Length; i++)
        {
            // pick asset for editing
            if (GUILayout.Button(assetFullPaths[i] + " - "+assets[i].name))
            {
                activeAsset = i;
            }
        }

        EditorGUILayout.EndVertical();
        if (curRecursiveDepth >= maxRecursiveDepth)
        {
            EditorGUILayout.LabelField("Max recursive depth reached. -> bug");
            
        }else if (activeAsset != -1 && activeAsset < assetFullPaths.Length)
        {
            // visualize selected node.
            UnityEngine.Object asset = assets[activeAsset];

            EditorGUILayout.BeginVertical();
            // visualized whole tree from selected node
            RecursiveShowAsset((AQNode)asset);

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            // display asset


            EditorGUILayout.EndVertical();
        }


        EditorGUILayout.EndHorizontal();

    }

    private void RecursiveShowAsset(AQNode asset)
    {
        if (curRecursiveDepth >= maxRecursiveDepth)
        {
            return;
        }
        curRecursiveDepth++;
        if (asset == null)
        {
            EditorGUILayout.LabelField("No value.");
            curRecursiveDepth--;
            return;
        }
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField(asset.name);
        EditorGUILayout.ObjectField(asset, asset.GetType(), false);
        if (GUILayout.Button(asset.name))
        {
            SetVisibilityForChildren(asset, true);
        }

        EditorGUILayout.EndVertical();

        // show list of children
        AQListNode listAsset = asset as AQListNode;
        AQDecoratorNode decAsset = asset as AQDecoratorNode;
        if (listAsset != null || decAsset != null)
        {
            EditorGUILayout.BeginVertical();

            if (listAsset != null)
            {
                for (int i = 0; i < listAsset.conditions.Length; i++)
                {
                    RecursiveShowAsset(listAsset.conditions[i]);
                }
            }
            else if (decAsset != null)
            {
                RecursiveShowAsset(decAsset.child);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();
        curRecursiveDepth--;

    }

    private void SetVisibilityForChildren(AQNode asset, bool v)
    {
        if (!displayChildren.ContainsKey(asset))
        {
            displayChildren.Add(asset, v);
        }
        else
        {
            displayChildren[asset] = v;
        }
    }

    private void LoadAssetsAndTheirPaths()
    {
        // load path
        path = EditorGUILayout.TextField(path);
        if (GUILayout.Button("Load from (MH3/Resources/ ...)"))
        {
            assets = Resources.LoadAll<UnityEngine.Object>(path);
            activeAsset = 0;
        }
        // find paths for assets
        
        assetFullPaths = new string[assets.Length];
        for (int i = 0; i < assets.Length; i++)
        {
            UnityEngine.Object parentObject = PrefabUtility.GetCorrespondingObjectFromSource(assets[i]);
            assetFullPaths[i] = AssetDatabase.GetAssetPath(assets[i]);
            assetFullPaths[i] = assetFullPaths[i].Substring(skipLength);
        }
    }

    Dictionary<AQNode, bool> displayChildren = new Dictionary<AQNode, bool>();

}

static class AQEditorRenderer {

    internal static void Show(AQNode asset)
    {
        
    }
}