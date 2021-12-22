using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootButtonUI : MonoBehaviour
{
    [SerializeField] private Button shootButton;
    [SerializeField] private TMP_Text cooldownText;

    private InGameCharacterPlayer targetPlayer;
    public void Show(InGameCharacterPlayer player)
    {
        gameObject.SetActive(true);
        targetPlayer = player;
    }

    public void Interactable(bool interact)
    {
        shootButton.interactable = interact;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        //masih harus dibenerin
        //cek ke playerfinder script 
        //semua objek dapat menjadi sasaran tembakan, namun hanya objek ingamecharactermovement yang terkena dampak tembakan
        if (targetPlayer != null)
        {
            if (!targetPlayer.IsShootable)
            {
                cooldownText.text = targetPlayer.ShootCooldown > 0 ? (System.Math.Round(targetPlayer.ShootCooldown, 1)).ToString() : "";
                shootButton.interactable = false;
            }
            else
            {
                cooldownText.text = "";
                shootButton.interactable = true;
            }
        }
    }

    public void OnClickButtonShoot()
    {
        //play animation attack
        //reduce 1 peluru 
        targetPlayer.Shoot();
    }
}
