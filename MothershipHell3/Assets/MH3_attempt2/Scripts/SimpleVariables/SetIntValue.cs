using UnityEngine;

public class SetIntValue : MonoBehaviour {
    [Header("Copy object from 'data from' to 'data to'")]
    [SerializeField] IntReference dataFrom;
    [SerializeField] IntReference dataTo;

    [Tooltip("0 Assign, 1 Add, 2 Subtract, 3 Multiply, 4 Divide")][SerializeField] int operation = 0;

    [SerializeField] bool debugSuccesses = true;

    public void Invoke()
    {
        if (dataTo != null && dataFrom != null)
        {
            if (operation == 0)
            {
                Log("SetValue invoked. " + name + " " + dataTo.Value + " -> " + dataFrom.Value);
                dataTo.Value = dataFrom.Value;
            }
            if (operation == 1)
            {
                Log("SetValue invoked. " + name + " " + dataTo.Value + " + " + dataFrom.Value+" = "+(dataTo.Value+ dataFrom.Value));
                dataTo.Value += dataFrom.Value;
            }
            if (operation == 2)
            {
                Log("SetValue invoked. " + name + " " + dataTo.Value + " - " + dataFrom.Value + " = " + (dataTo.Value + dataFrom.Value));
                dataTo.Value -= dataFrom.Value;
            }
            if (operation == 3)
            {
                Log("SetValue invoked. " + name + " " + dataTo.Value + " * " + dataFrom.Value + " = " + (dataTo.Value + dataFrom.Value));
                dataTo.Value *= dataFrom.Value;
            }
            if (operation == 4)
            {
                Log("SetValue invoked. " + name + " " + dataTo.Value + " / " + dataFrom.Value + " = " + (dataTo.Value + dataFrom.Value));
                dataTo.Value /= dataFrom.Value;
            }
        }
        else Debug.LogError("Some of data is null on SETOBJECTVALUE." + dataTo.Value + dataFrom.Value, this);
    }

    void Log(string msg)
    {
        if (debugSuccesses) 
            Debug.Log(msg);
    }
}
