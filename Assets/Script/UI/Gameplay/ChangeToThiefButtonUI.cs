using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToThiefButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private InGameCharacterPlayer targetPlayer;
    public void Show(InGameCharacterPlayer player)
    {
        gameObject.SetActive(true);
        targetPlayer = player;
    }

    public void Interactable(bool interact)
    {
        button.interactable = interact;
    }

    public void OnClickButtonChangeToThief()
    {
        //play animation attack
        //reduce 1 peluru 
        targetPlayer.ChangeToThief();
    }
}
