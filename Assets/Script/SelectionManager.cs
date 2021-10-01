using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] string selectableTag = "Selectable";
    ISelectionResponse _selectionResponse;
    Transform _selection;
    void Update()
    {
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ini mungkin harus diganti inputnya
        _selection = null;
        if(Physics.Raycast(ray,out var hit)) //Kasih batas range disini
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }
        if (_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }
    }
}