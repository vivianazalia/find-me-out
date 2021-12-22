using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BomButtonUI : MonoBehaviour
{
    [SerializeField] Button shootButton;
    [SerializeField] TMP_Text cooldownText;

    private InGameCharacterPlayer targetPlayer;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(InGameCharacterPlayer player)
    {
        gameObject.SetActive(true);
        targetPlayer = player;
    }

    public void SetTargetPlayer(InGameCharacterPlayer player)
    {
        targetPlayer = player;
    }

    public void Interactable(bool interact)
    {
        shootButton.interactable = interact;
    }

    private void Update()
    {
        Throw();
    }

    private void Throw()
    {
        //masih harus dibenerin
        //cek ke playerfinder script 
        //semua objek dapat menjadi sasaran tembakan, namun hanya objek ingamecharactermovement yang terkena dampak tembakan
        if (targetPlayer != null)
        {
            if (!targetPlayer.CanThrowBom)
            {
                cooldownText.text = targetPlayer.BomCooldown > 0 ? ((int)targetPlayer.BomCooldown).ToString() : "";
                shootButton.interactable = false;
            }
            else
            {
                cooldownText.text = "";
                shootButton.interactable = true;
            }
        }
    }

    public void OnClickThrowBomb()
    {
        targetPlayer.ThrowBomb();
    }
}
