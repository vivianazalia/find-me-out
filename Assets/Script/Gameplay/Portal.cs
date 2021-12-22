using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private BoxCollider2D collider;
    private SpriteRenderer sprite;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OpenPortal()
    {
        collider.enabled = false;
        sprite.enabled = false;
    }
}
