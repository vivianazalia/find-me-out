using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HealthUI : UI
{
    [SerializeField] private GameObject healthUI;
    [SerializeField] private Slider healthValue;

    private InGamePlayerMovement player;

    public override void Show(InGamePlayerMovement target)
    {
        healthUI.SetActive(true);
        player = target;
    }

    //[TargetRpc]
    public void UpdateUI()
    {
        PlayerThief p = player.GetComponent<PlayerThief>();
        if(p != null)
        {
            healthValue.value = p.GetCurrentHealth();
        }
    }
}
