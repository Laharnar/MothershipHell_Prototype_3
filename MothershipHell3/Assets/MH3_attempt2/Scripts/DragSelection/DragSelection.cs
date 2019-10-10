﻿using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Allows mouse based drag selection.
/// 
/// Relies on ISelectable to distribute calls.
/// </summary>
public class DragSelection : MonoBehaviour {
    bool click = false;

    List<ISelectable> selectedGameObjects;
    Camera mainCam;

    [SerializeField] Rect selectArea;
    [SerializeField] List<StandardSelectableMono> selectableGameBehaviours;

    // dev variables
    [SerializeField] List<GameObject> dev_currentlySelected;

    private void Awake()
    {
        selectableGameBehaviours = new List<StandardSelectableMono>();
        selectedGameObjects = new List<ISelectable>();
        dev_currentlySelected = new List<GameObject>();
        selectArea = new Rect();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            click = true;

            for (int i = 0; i < selectedGameObjects.Count; i++)
            {
                selectedGameObjects[i].OnDeselected();
            }
            selectedGameObjects.Clear();
            dev_currentlySelected.Clear();

            selectArea.x = Input.mousePosition.x;
            selectArea.y = Input.mousePosition.y;
            selectArea.width = 0;
            selectArea.height = 0;
        }
        if (click && Input.GetKey(KeyCode.Mouse0))
        {
            selectArea.width = Input.mousePosition.x- selectArea.x;
            selectArea.height = Input.mousePosition.y- selectArea.y ;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            click = false;
            selectArea.width = Input.mousePosition.x-selectArea.x;
            selectArea.height = Input.mousePosition.y-selectArea.y;
            Rect selectionAreaFinal = selectArea;

            // ensure selection is positive
            if (selectionAreaFinal.width < 0)
            {
                selectionAreaFinal.width *= -1;
                selectionAreaFinal.x -= Mathf.Abs(selectionAreaFinal.width);
            }
            if (selectionAreaFinal.height< 0)
            {
                selectionAreaFinal.height *= -1;
                selectionAreaFinal.y -= Mathf.Abs(selectionAreaFinal.height);
            }
            // check which objects are selected.
            for (int i = 0; i < selectableGameBehaviours.Count; i++)
            {
                Vector2 objInScreenCoordinates;
                objInScreenCoordinates = mainCam.WorldToScreenPoint(
                    selectableGameBehaviours[i].WorldPos);
                //Debug.Log(objInScreenCoordinates); // dev
                if (selectionAreaFinal.Contains(objInScreenCoordinates))
                {
                    Debug.Log("Selected "+i+" "+selectableGameBehaviours[i]);
                    selectedGameObjects.Add(selectableGameBehaviours[i]);
                    dev_currentlySelected.Add(selectableGameBehaviours[i].gameObject);
                }
            }
            for (int i = 0; i < selectedGameObjects.Count; i++)
            {
                selectedGameObjects[i].OnSelected();
            }
        }
    }

    public void RegisterAsSelectable(StandardSelectableMono obj)
    {
        selectableGameBehaviours.Add(obj);
    }
}
