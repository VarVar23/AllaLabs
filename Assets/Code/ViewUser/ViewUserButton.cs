using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ViewUserButton : MonoBehaviour
{
    [SerializeField] private Button _userButton;
    [SerializeField] private TMP_Text _userName;

    public int UserID;
    public ViewUserPanel _viewUserPanel;
    private List<User> _listUser;


    void Start()
    {
        _listUser = SaveToJSON.Users.Users;
        _userName.text = _listUser[UserID].Name;

        _viewUserPanel = FindObjectOfType<ViewUserPanel>(true);
        _userButton.onClick.AddListener(ActivePanel);
    }

    private void ActivePanel()
    {
        _viewUserPanel.gameObject.SetActive(true);
        _viewUserPanel.UserID = UserID;
        _viewUserPanel.StartUserParams();
    }
}
