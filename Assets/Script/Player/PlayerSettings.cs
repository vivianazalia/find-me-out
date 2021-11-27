using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSettings : MonoBehaviour
{
    [SerializeField] int lives = 2;
    [SerializeField] bool isAI = false;
    [SerializeField] float _lookSpeed = 1.0f;
    [SerializeField] float _rotationSpeed = 4.0f;
    [SerializeField] float _walkSpeed = 4.0f;
    [SerializeField] float _runSpeed = 8.0f;
    [SerializeField] float _gravityValue = -9.0f;
    [SerializeField] float _jumpHeight = 9.0f;

    public static string nickname;
    public static string playerPrefsNameKey = "PlayerName";
    public static string firstRunAppKey = "FirstRunApp";
    public enum PlayerRole
    {
        Unassigned,
        Hider,
        Seeker,
        Ghost
    }
    [SerializeField] PlayerRole role = PlayerRole.Unassigned;

    public int Lives { get { return lives; } }
    public PlayerRole Role { get { return role; } }
    public bool IsAI { get { return isAI; } }
    public float lookSpeed { get { return _lookSpeed; } }
    public float rotationSpeed { get { return _rotationSpeed; } }
    public float walkSpeed { get { return _walkSpeed; } }
    public float runSpeed { get { return _runSpeed; } }
    public float gravityValue { get { return _gravityValue; } }
    public float jumpHeight { get { return _jumpHeight; } }
}
