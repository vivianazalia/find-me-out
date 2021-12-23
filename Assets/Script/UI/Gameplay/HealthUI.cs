using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private InGameCharacterPlayer targetPlayer;

    [SerializeField] private Image healthBar;
    [SerializeField] private Slider slider;
    public void Hide()
    {
        gameObject.SetActive(false);
        //targetPlayer = player;
    }

    public void Show(InGameCharacterPlayer player)
    {
        gameObject.SetActive(true);
        targetPlayer = player;
    }

    public void UpdateHealthBar(float health)
    {
        //healthBar.fillAmount = health;
        slider.value = health;
        if (health > .5f)
        {
            healthBar.color = Color.green;
        }
        else if (health > .2f)
        {
            healthBar.color = Color.yellow;
        }
        else if (health > 0)
        {
            healthBar.color = Color.red;
        }
    }
}
