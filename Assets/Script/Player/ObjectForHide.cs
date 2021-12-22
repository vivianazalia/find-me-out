using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectForHide : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;

    public int index;

    public TMP_Text PopupText { get { return popupText; } }

    private float doubleClickTime = 0.25f;
    private float lastClickTime;

    private InGameCharacterPlayer targetPlayer;

    private void Start()
    {
        gameObject.tag = "ObjectForHide";
    }

    public void SetTargetPlayer(InGameCharacterPlayer player)
    {
        if (targetPlayer == null)
        {
            targetPlayer = player;
            //Debug.Log("Set Target player : " + targetPlayer + " Id: " + targetPlayer.netId);
        }
    }

    public void RemoveTargetPlayer(InGameCharacterPlayer player)
    {
        if (targetPlayer != null)
        {
            targetPlayer = null;
            //Debug.Log("Remove Target player : " + targetPlayer + " Id: " + targetPlayer.netId);
        }
    }

    private void OnMouseDown()
    {
        float offsetTime = Time.time - lastClickTime;

        if (offsetTime < doubleClickTime)
        {
            if (targetPlayer != null)
            {
                targetPlayer.Hide(index);
            }
        }

        lastClickTime = Time.time;
    }
}
