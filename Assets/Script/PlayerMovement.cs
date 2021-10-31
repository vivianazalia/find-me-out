using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    private Vector3 movement;

    private Camera mainCam;
    private Vector3 camPos;

    private float camLeftMax = -6.41f;
    private float camRightMax = 6.41f;
    private float camTopMax = 3.66f;
    private float camButtomMax = -3.68f;

    private void Start()
    {
        if (hasAuthority)
        {
            mainCam = FindObjectOfType<Camera>();
            mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, mainCam.transform.position.z);
            camPos = mainCam.transform.position;
        }
    }

    private void Update()
    {
        if (!hasAuthority) return;

        PlayerMove();

        CameraPosition();
    }

    private void CameraPosition()
    {
        camPos = new Vector3(transform.position.x, transform.position.y, mainCam.transform.position.z);

        if (camPos.x > camRightMax && camPos.y > camTopMax)
        {
            camPos = new Vector3(camRightMax, camTopMax, mainCam.transform.position.z);
        }
        else if (camPos.x < camLeftMax && camPos.y > camTopMax)
        {
            camPos = new Vector3(camLeftMax, camTopMax, mainCam.transform.position.z);
        }
        else if (camPos.x > camRightMax && camPos.y < camButtomMax)
        {
            camPos = new Vector3(camRightMax, camButtomMax, mainCam.transform.position.z);
        }
        else if (camPos.x < camLeftMax && camPos.y < camButtomMax)
        {
            camPos = new Vector3(camLeftMax, camButtomMax, mainCam.transform.position.z);
        }
        else if (camPos.x > camRightMax)
        {
            camPos = new Vector3(camRightMax, transform.position.y, mainCam.transform.position.z);
        }
        else if (camPos.x < camLeftMax)
        {
            camPos = new Vector3(camLeftMax, transform.position.y, mainCam.transform.position.z);
        }
        else if (camPos.y > camTopMax)
        {
            camPos = new Vector3(transform.position.x, camTopMax, mainCam.transform.position.z);
        }
        else if (camPos.y < camButtomMax)
        {
            camPos = new Vector3(transform.position.x, camButtomMax, mainCam.transform.position.z);
        }

        mainCam.transform.position = camPos;
    }

    private Vector3 GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        return movement;
    }

    private void PlayerMove()
    {
        transform.position += GetInput() * speed * Time.deltaTime;
    }
}
