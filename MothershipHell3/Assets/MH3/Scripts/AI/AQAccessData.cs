using System.Collections.Generic;
using UnityEngine;
public class AQAccessData {
    Dictionary<string, int> ints = new Dictionary<string, int>();
    Dictionary<string, bool> bools = new Dictionary<string, bool>();
    Dictionary<string, object> objs = new Dictionary<string, object>();
    Dictionary<string, Transform> transforms = new Dictionary<string, Transform>();

    internal object Get(string code)
    {
        if (ints.ContainsKey(code))
        {
            return ints[code];
        }
        if (bools.ContainsKey(code))
        {
            return bools[code];
        }
        if (objs.ContainsKey(code))
        {
            return objs[code];
        }
        if (transforms.ContainsKey(code))
        {
            return transforms[code];
        }
        Debug.Log("Resource for code doesn't exist :: "+code);
        return null;
    }

    public void Set(string code, int value)
    {
        if (!ints.ContainsKey(code))
        {
            ints.Add(code, value);
        }
        else ints[code] = value;
    }

    public void SetBool(string code, bool value)
    {
        if (!bools.ContainsKey(code))
        {
            bools.Add(code, value);
        }
        else bools[code] = value;
    }

    public void Set(string code, object value)
    {
        if (!bools.ContainsKey(code))
        {
            objs.Add(code, value);
        }
        else objs[code] = value;
    }

    public void Set(string code, Transform value)
    {
        if (!transforms.ContainsKey(code))
        {
            transforms.Add(code, value);
        }
        else transforms[code] = value;
    }
}
