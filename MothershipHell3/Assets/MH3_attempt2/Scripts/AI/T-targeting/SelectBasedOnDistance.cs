
using UnityEngine;
using UnityEngine.Events;

// put on specific game obejct, that's selectable.
public class SelectBasedOnDistance:MonoBehaviour {
    [SerializeField] ObjectReference selectedSave;// save into
    [Tooltip("Which value to save. obsolete")][SerializeField] GameObject whenSelectedSetTo;
    [Tooltip("new variable")][SerializeField] GameObject saveValue;
    [SerializeField] FloatReference selectionDistance;
    [SerializeField] KeyCode key = KeyCode.Mouse0;

    Camera cam;

    public UnityEvent onSelect;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePos, transform.position) < selectionDistance.Value)
            {
                if (saveValue)
                    selectedSave.Value = saveValue;
                else 
                    selectedSave.Value = whenSelectedSetTo;
                if (onSelect != null)
                    onSelect.Invoke();
            }
        }
    }
}
