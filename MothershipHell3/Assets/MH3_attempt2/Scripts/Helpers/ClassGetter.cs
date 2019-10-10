using System;
using System.Collections.Generic;
using UnityEngine;

public static class ClassGetter {

}

/// <summary>
/// Allows quick access and caching of last accessed class of certain type.
/// Very useful for managers accessing.
/// </summary>
public static class ClassCache {

    static Dictionary<Type, MonoBehaviour> cache = new Dictionary<Type, MonoBehaviour>();

    static MonoBehaviour lastGet;

    public static T LastResult<T>(this MonoBehaviour src) where T : MonoBehaviour
    {
        return (T)lastGet;
    }

    /// <summary>
    /// Use for quickly accessing unique classes, like managers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src"></param>
    /// <returns></returns>
    public static T GetUniqueClass<T>(this MonoBehaviour src) where T: MonoBehaviour
    {
        if (cache.ContainsKey(typeof(T))) {
            if (cache[typeof(T)] == null)
            {
                cache[typeof(T)] = GameObject.FindObjectOfType<T>();
            }
            return (T)cache[typeof(T)];
        }
        T t = GameObject.FindObjectOfType<T>();
        Debug.Log("Caching new unique class "+t +" of type "+typeof(T));
        cache.Add(typeof(T), t);
        return t;
    }

    /// <summary>
    /// Use for quickly accessing unique classes, like managers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src"></param>
    /// <returns></returns>
    public static bool TryGetUniqueClass<T>(this MonoBehaviour src) where T : MonoBehaviour
    {
        T result = src.GetUniqueClass<T>();
        lastGet = result;
        if (result == null)
        {
            Debug.Log("Getting unique class of type "+typeof(T)+" failed -> null.");
            return false;
        }
        return true;
    }
}