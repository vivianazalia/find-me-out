using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputName : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private Button enterGameButton = null;

    private string playerPrefsNameKey = "PlayerName";

    public static string DisplayName { get; set; }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(playerPrefsNameKey)) { return; }

        string name = PlayerPrefs.GetString(playerPrefsNameKey);

        inputField.text = name;
        SetPlayerName();
    }

    public void SetPlayerName()
    {
        enterGameButton.interactable = !string.IsNullOrEmpty(inputField.text);
    }

    public void SavePlayerName()
    {
        DisplayName = inputField.text;
        PlayerPrefs.SetString(playerPrefsNameKey, DisplayName);
    }
   
    void Start()
    {
        SetUpInputField();
    }
}
