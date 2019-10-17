using UnityEngine;

public class SetObjectValue : MonoBehaviour {
    [Header("Copy object from 'data from' to 'data to'")]
    [SerializeField] ObjectReference dataFrom;
    [SerializeField] ObjectReference dataTo;

    public void Invoke()
    {
        if (dataTo != null && dataFrom != null) {
            if (dataFrom.Value != null)
                dataTo.Value = dataFrom.Value;
            else Debug.LogError("DataFromValue is null." + dataFrom, this);
        } else Debug.LogError("Some of data is null on SETOBJECTVALUE." + dataTo.Value + dataFrom.Value, this);
    }
}
