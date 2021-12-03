using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public enum PlayerType
{
    participant,
    police,
    thief
}

public struct Player : NetworkMessage
{
    PlayerType type;
}

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

    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    private TMP_Text nicknameText;

    [SerializeField] protected Animator anim;

    public void SetNickname_Hook(string oldValue, string newValue)
    {
        nicknameText.text = newValue;
    }

    protected virtual void Start()
    {
        if (hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
            cam.transform.localScale = new Vector3(1, 1, 1);
        }

        if (isLocalPlayer)
        {
            Debug.Log("ID : " + netId);
        }
    }

    private void Update()
    {
        if (!hasAuthority) return;

        PlayerMove();

        //CameraPosition();
    }

    private void CameraPosition()
    {
        camPos = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);

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
        if(GetInput() == Vector3.zero)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
        transform.position += GetInput() * speed * Time.deltaTime;
    }
}
