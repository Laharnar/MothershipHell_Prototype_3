
using UnityEngine;
// put and act as manager that edits data.
public class SelectInGroupBasedOnDistance : MonoBehaviour {
    [SerializeField] ObjectReferenceArray canSelectFrom;
    [SerializeField] FloatReference selectionDistance;
    [SerializeField] ObjectReference selectedSave;
    [SerializeField] KeyCode key = KeyCode.Mouse0;

    Camera cam;
    Vector2 mousePos;
    ObjectValue last;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if(canSelectFrom.CheckConditionAny(DistanceToMouseAcceptable))
            {
                selectedSave.Value = last.value;
            }
        }
    }

    bool DistanceToMouseAcceptable(ObjectValue obj)
    {
        last = obj;
        return Vector2.Distance(mousePos, obj.value.transform.position) < selectionDistance.Value;
    }
}
