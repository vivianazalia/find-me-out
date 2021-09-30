using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Settings", fileName = "PlayerData")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] int lives = 2;
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] bool isAI = false;
    public enum PlayerRole
    {
        Unassigned,
        Hider,
        Seeker,
        Ghost
    }
    [SerializeField] PlayerRole role = PlayerRole.Unassigned;

    public int Lives { get { return lives; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public PlayerRole Role { get { return role; } }
    public bool IsAI { get { return isAI; } }
}
