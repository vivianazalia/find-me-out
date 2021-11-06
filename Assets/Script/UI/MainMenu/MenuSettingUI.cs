using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSettingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_InputField inputNewNickname;
    [SerializeField] private GameObject inputNewNicknamePanel;

    private void Start()
    {
        nickname.text = PlayerSettings.nickname;
    }

    public void ChangeNickname()
    {
        inputNewNicknamePanel.SetActive(true);
    }

    public void SaveNewNickname()
    {
        if(inputNewNickname.text != "")
        {
            PlayerSettings.nickname = inputNewNickname.text;
            PlayerPrefs.SetString(PlayerSettings.playerPrefsNameKey, inputNewNickname.text);
            nickname.text = PlayerSettings.nickname;
        }

        inputNewNicknamePanel.SetActive(false);
    }
}
