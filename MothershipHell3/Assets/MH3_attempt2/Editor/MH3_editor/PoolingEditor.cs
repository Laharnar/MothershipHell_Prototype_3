using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Text;

public class PoolingWindow : EditorWindow {

    public static PoolingWindow window;
    ReadOnlyCollection<DebugMessage> debugMessages;
    ReadOnlyDictionary<string, Pooling.PoolInstance> cache;

    Vector2 scroll;

    [MenuItem("Custom/Pooling")]
    public static void ShowWindow()
    {
        window = GetWindow<PoolingWindow>(typeof(PoolingWindow));
        window.autoRepaintOnSceneChange = true;
    }
    
    void OnGUI()
    {
        if (Application.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();



            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Pooling");
            cache = this.GetUniqueClass<Pooling>().Cache;
            foreach (Pooling.PoolInstance value in cache.Values)
            {
                StringBuilder show = new StringBuilder();
                show.Append(value.Group);
                show.Append(": ");
                EditorGUILayout.LabelField(show.ToString());
                show.Clear();
                int pooledCount = 0;
                for (int i = 0; i < value.CreatedObjects.Count; i++)
                {
                    Pooling.PoolItem item = value.CreatedObjects[i];
                    bool isPooled = item.isInPoolOnStandby;
                    if (isPooled)
                        pooledCount++;
                }
                show.Append("pooled:");
                show.Append(pooledCount);
                show.Append("/");
                show.Append(value.CreatedObjects.Count);
                EditorGUILayout.LabelField(show.ToString());
                show.Clear();
            }
            EditorGUILayout.LabelField("Some objects can be marked as pooled, but not listed here, because they aren't registered yet.");
            EditorGUILayout.EndVertical();



            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Pool debugger");
            scroll = EditorGUILayout.BeginScrollView(scroll);
            debugMessages = this.GetUniqueClass<Pooling>().DebugMessages;

            for (int i = debugMessages.Count - 1; i >= 0; i--)
            {
                DebugMessage msg = debugMessages[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(msg.Content);
                if (msg.UseObjTarget)
                    EditorGUILayout.ObjectField(
                        msg.ObjTarget,
                        msg.ObjTarget.GetType(), true);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();



            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField("Press play to show POOLING ...");
        }
    }
}
