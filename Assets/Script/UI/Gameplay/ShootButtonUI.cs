using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ShootButtonUI : UI
{
    [SerializeField] private Button shootBtn;

    private InGamePlayerMovement player;

    public override void Show(InGamePlayerMovement target)
    {
        shootBtn.gameObject.SetActive(true);
        player = target;
    }

    public void OnClickButtonShoot()
    {
        player.Shoot();
    }
}
