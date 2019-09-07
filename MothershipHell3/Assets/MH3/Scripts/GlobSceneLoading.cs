/* Created by: Marko Laharnar, 6.9.2019
 * 
 * Purpose: 
 * Allows any script to load a scene.
 * + Supports UI, like button events.
 * 
 * Setup:
 * Put a single instance in the scene. (Multiple instances are allowed)
 * 
 * 
 * */

using UnityEngine;

/// <summary>
/// Allows any script to load a scene.
/// </summary>
public class GlobSceneLoading : MonoBehaviour
{
    public void UILoadScene(string sceneName)
    {
        LoadScene(sceneName);
    }

    public static void LoadScene(string sceneName)
    {
        if (sceneName == null) return;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
