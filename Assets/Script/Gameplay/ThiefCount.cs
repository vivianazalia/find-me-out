using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ThiefCount : NetworkBehaviour
{
    public static ThiefCount instance = null;

    [SyncVar(hook = nameof(UpdateThiefCountText_Hook))]
    public int thiefCount;

    [Header("UI")]
    [SerializeField] private TMP_Text thiefCountText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateThiefCountText_Hook(int oldValue, int newValue)
    {
        thiefCountText.text = "Thief Remains : " + newValue;
    }
}
