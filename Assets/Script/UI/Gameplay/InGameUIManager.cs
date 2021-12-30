using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [SerializeField] private ShootButtonUI shootButton;
    [SerializeField] private BombButtonUI bomButton;
    [SerializeField] private ChangeToThiefButtonUI changeToThiefButton;
    [SerializeField] private HealthUI healthBar;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelLeave;
    public ShootButtonUI ShootButton { get { return shootButton; } }
    public BombButtonUI BomButton { get { return bomButton; } }
    public ChangeToThiefButtonUI ChangeToThiefButton { get { return changeToThiefButton; } }
    public HealthUI HealthBar { get { return healthBar; } }
    public GameObject PanelWin { get { return panelWin; } }
    public GameObject PanelLose { get { return panelLose; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //if (Application.platform == RuntimePlatform.Android)
        //{
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panelLeave.SetActive(true);
            }
        //}
    }

    public void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }

    public void ShowPanelLose()
    {
        panelLose.SetActive(true);
    }

    public void LeaveGame()
    {
        NetworkClient.Disconnect();
        NetworkManager.singleton.StopClient();
    }
}
