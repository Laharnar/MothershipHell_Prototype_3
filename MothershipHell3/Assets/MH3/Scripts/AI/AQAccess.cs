using System;
using System.Collections.Generic;
using UnityEngine;

public class AQAccess:MonoBehaviour {
    public static AQAccess Ins {
        get {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<AQAccess>();
            }
            return instance;
        }
    }

    static AQAccess instance;
    public static IAQAccessible ActiveSource { get; internal set; }

    // every object that can be accessed individually by ai, should have IAQAccessible assigned.
    Dictionary<IAQAccessible, AQAccessData> AQSourceLib = new Dictionary<IAQAccessible, AQAccessData>();

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        ActiveSource = null;
        instance = null;
    }

    public void iRegister(IAQAccessible source)
    {
        AQSourceLib.Add(source, new AQAccessData());
    }

    public object iGetData(IAQAccessible source, string code)
    {
        if (source == null)
        {
            Debug.Log("Source function parameter is null. Returning null.");
            return null;
        }
        if (!AQSourceLib.ContainsKey(source))
            return null;
        object data = AQSourceLib[source].Get(code);
        if (data == null)
        {
            Debug.Log("Data doesn't exist "+ source.Obj +" "+ code, source.Obj);
        }
        return data;
    }

    public object iGetData(string code)
    {
        return iGetData(ActiveSource, code);
    }

    public static void Register(IAQAccessible source)
    {
        Ins.iRegister(source);
    }

    public static object GetData(IAQAccessible source, string code)
    {
        return Ins.iGetData(source, code);
    }

    public static object GetData(string code)
    {
        return Ins.iGetData(code);
    }

    public void SetData(IAQAccessible source, string code, int value)
    {
        if (source == null)
        {
            Debug.Log("Source function parameter is null. Aborting.");
            return;
        }
        if (!AQSourceLib.ContainsKey(source))
        {
            Debug.Log("Source " + source + " isn't manually inserted into db. Aborting.");
            return;
        }
        AQSourceLib[source].Set(code, value);
    }

    public void SetBoolData(IAQAccessible source, string code, bool value)
    {
        if (source == null)
        {
            Debug.Log("Source function parameter is null. Aborting.");
            return;
        }
        if (!AQSourceLib.ContainsKey(source))
        {
            Debug.Log("Source " + source + " isn't manually inserted into db. Aborting.");
            return;
        }
        AQSourceLib[source].SetBool(code, value);
    }

    public void SetData(IAQAccessible source, string code, object value)
    {
        if (source == null)
        {
            Debug.Log("Source function parameter is null. Aborting.");
            return;
        }
        if (!AQSourceLib.ContainsKey(source))
        {
            Debug.Log("Source " + source + " isn't manually inserted into db. Aborting.");
            return;
        }
        AQSourceLib[source].Set(code, value);
    }

    public void SetData(IAQAccessible source, string code, Transform value)
    {
        if(source == null)
        {
            Debug.Log("Source function parameter is null. Aborting.");
            return;
        }
        if (!AQSourceLib.ContainsKey(source))
        {
            Debug.Log("Source "+source+" isn't manually inserted into db. Aborting.");
            return;
        }
        AQSourceLib[source].Set(code, value);
    }
}
