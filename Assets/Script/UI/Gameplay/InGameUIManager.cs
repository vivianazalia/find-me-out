using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    [SerializeField] private ShootButtonUI shootBtn;

    public ShootButtonUI shootButton { get { return shootBtn; } }
}
