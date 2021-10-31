using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraRotation : NetworkBehaviour
{
    private PlayerMovement[] targets;
    public PlayerMovement targetObj = null;

    public float speedH = 2f;
    public float speedV = 2f;

    private float yaw = 0f;
    private float pitch = 0f;

    private Vector3 cameraOffset;

    private float smoothFactor = 0.5f;

    private float zMax = -12f;
    private float zMin = 2.84f;
    private float xMax = 7.31f;
    private float xMin = -6.8f;

    private void Start()
    {
        targets = FindObjectsOfType<PlayerMovement>();

        foreach (PlayerMovement target in targets)
        {
            if (target.hasAuthority)
            {
                targetObj = target;
                transform.position = new Vector3(targetObj.transform.position.x, transform.position.y, transform.position.z);
                cameraOffset = transform.position - targetObj.transform.position;
            }
        }
    }

    private void Update()
    {
        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");
        //
        //transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    private void LateUpdate()
    {
        Vector3 newPosition = targetObj.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
