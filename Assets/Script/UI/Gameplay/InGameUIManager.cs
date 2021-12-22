using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [SerializeField] private ShootButtonUI shootButton;
    [SerializeField] private BomButtonUI bomButton;
    //[SerializeField] private BulletUI bulletCount;
    [SerializeField] private HealthUI healthBar;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    public ShootButtonUI ShootButton { get { return shootButton; } }
    public BomButtonUI BomButton { get { return bomButton; } }
    //public BulletUI BulletCount { get { return bulletCount; } }
    public HealthUI HealthBar { get { return healthBar; } }
    public GameObject PanelWin { get { return panelWin; } }
    public GameObject PanelLose { get { return panelLose; } }

    private void Awake()
    {
        instance = this;
    }

    public void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }

    public void ShowPanelLose()
    {
        panelLose.SetActive(true);
    }
}
