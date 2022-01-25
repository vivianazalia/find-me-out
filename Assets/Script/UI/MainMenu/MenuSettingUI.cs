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
        nickname.text = GameSetting.nickname;
    }

    public void ChangeNickname()
    {
        inputNewNicknamePanel.SetActive(true);
    }

    public void SaveNewNickname()
    {
        if(inputNewNickname.text != "")
        {
            GameSetting.nickname = inputNewNickname.text;
            PlayerPrefs.SetString(GameSetting.playerPrefsNameKey, inputNewNickname.text);
            nickname.text = GameSetting.nickname;
        }

        inputNewNicknamePanel.SetActive(false);
    }
}
