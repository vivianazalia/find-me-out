using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OpenPortal()
    {
        var colliders = GetComponents<BoxCollider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
    }
}
