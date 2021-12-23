using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    protected TMP_Text nicknameText;

    protected Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetNickname_Hook(string oldValue, string newValue)
    {
        nicknameText.text = newValue;
    }
}
