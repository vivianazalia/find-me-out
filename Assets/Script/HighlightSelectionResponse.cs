using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] Material highlightMaterial;
    Dictionary<GameObject, Material> originalMaterials;
    
    public void OnSelect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if(selectionRenderer != null && !originalMaterials.ContainsKey(selection.gameObject))
        {
            originalMaterials.Add(selection.gameObject, selectionRenderer.material);
            selectionRenderer.material = highlightMaterial;
        }
    }
    public void OnDeselect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null && originalMaterials.TryGetValue(selection.gameObject, out var _material))
        {
            selectionRenderer.material = _material;
            originalMaterials.Remove(selection.gameObject);
        }
    }
}
