using UnityEngine;

public class SetBoolValue : MonoBehaviour {
    [Header("Copy object from 'data from' to 'data to'")]
    [SerializeField] BoolReference dataFrom;
    [SerializeField] BoolReference dataTo;

    public void Invoke()
    {
        if (dataTo != null && dataFrom != null)
        {
            dataTo.Value = dataFrom.Value;
        }
        else Debug.LogError("Some of data is null on SETOBJECTVALUE." + dataTo.Value + dataFrom.Value, this);
    }
}
