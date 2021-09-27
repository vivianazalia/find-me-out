using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    Vector3 movement;

    Rigidbody rbPlayer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        rbPlayer.MovePosition(rbPlayer.position + movement * speed * Time.deltaTime);
    }

    void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }
}
