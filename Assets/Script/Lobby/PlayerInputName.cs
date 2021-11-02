using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputName : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button enterGameButton = null;
    [SerializeField] private GameObject panelMenu;

    private void Update()
    {
        enterGameButton.interactable = !string.IsNullOrEmpty(nameInputField.text);
    }

    public void OnClickStartButton()
    {
        if(nameInputField.text != "")
        {
            PlayerSettings.nickname = nameInputField.text;
            PlayerPrefs.SetString(PlayerSettings.playerPrefsNameKey, PlayerSettings.nickname);
            gameObject.SetActive(false);
            panelMenu.SetActive(true);
            PlayerPrefs.SetInt(PlayerSettings.firstRunAppKey, 1);
        }
    }
}
