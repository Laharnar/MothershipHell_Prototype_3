using UnityEngine;

public static class Q {
    public static void Log(string msg)
    {
        Debug.Log("L: "+msg);
    }
    public static void Log(string msg, Object context)
    {
        Debug.Log("L+O: "+msg, context);
    }
}