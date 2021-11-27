using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    CinemachineFreeLook cinemachine;
    InputManager inputManager;
    PlayerSettings playerSettings;

    // Start is called before the first frame update
    void Awake()
    {
        cinemachine = GetComponent<CinemachineFreeLook>();
        inputManager = FindObjectOfType<Player>().GetComponent<InputManager>(); //gimana cara cari player yg main?
        playerSettings = FindObjectOfType<Player>().GetComponent<PlayerSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = inputManager.RightStickInput;
        cinemachine.m_XAxis.Value += delta.x * 200 * playerSettings.lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += delta.y * playerSettings.lookSpeed * Time.deltaTime;
    }
}
