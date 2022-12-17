using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ViewUserPanel : MonoBehaviour
{
    [HideInInspector] public int UserID;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _userText;
    [SerializeField] private Toggle _toggleBlockUser;
    [SerializeField] private Toggle _toggleSetRestrictions;
    [SerializeField] private Button _exitButton;

    private List<User> _listUser;

    public void StartUserParams()
    {
        _listUser = SaveToJSON.Users.Users;

        _exitButton.onClick.AddListener(ClosePanel);
        _toggleBlockUser.onValueChanged.AddListener(ChangeToggleBlockParams);
        _toggleSetRestrictions.onValueChanged.AddListener(ChangeToggleRestrictionsParams);

        _userText.text = "Пользователь " + _listUser[UserID].Name;
        _toggleBlockUser.isOn = _listUser[UserID].Blocked;
        _toggleSetRestrictions.isOn = _listUser[UserID].Restrictions;
    }

    private void ChangeToggleBlockParams(bool value) => SaveToJSON.SaveToggleBlock(UserID, value);

    private void ChangeToggleRestrictionsParams(bool value) => SaveToJSON.SaveToggleRestrictions(UserID, value);

    private void ClosePanel() => _panel.SetActive(false);

}
