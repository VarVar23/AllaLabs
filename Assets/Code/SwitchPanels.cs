using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class SwitchPanels : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _mainEntry;
    [SerializeField] private Button _adminChangePassword;
    [SerializeField] private Button _adminViewUsers;
    [SerializeField] private Button _adminAddUsers;
    [SerializeField] private Button _adminClose;

    [SerializeField] private Button _adminChangePasswordComplete;
    [SerializeField] private Button _adminChangePasswordClose;
    [SerializeField] private Button _adminViewUsersClose;
    [SerializeField] private Button _adminAddUsersComlete;
    [SerializeField] private Button _adminAddUsersClose;

    [SerializeField] private Button _userChangePassword;
    [SerializeField] private Button _userChangePasswordClose;
    [SerializeField] private Button _userChangePasswordComplete;
    [SerializeField] private Button _userClose;


    [Header("Panels")]
    [SerializeField] private GameObject _entryPanel;
    [SerializeField] private GameObject _adminPanel;
    [SerializeField] private GameObject _changePasswordAdminPanel;
    [SerializeField] private GameObject _viewUserPanel;
    [SerializeField] private GameObject _addUserPanel;
    [SerializeField] private GameObject _userPanel;
    [SerializeField] private GameObject _changePasswordUserPanel;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _newUser;
    [SerializeField] private TMP_InputField _oldPasswordAdmin;
    [SerializeField] private TMP_InputField _newPasswordAdmin1;
    [SerializeField] private TMP_InputField _newPasswordAdmin2;
    [SerializeField] private TMP_InputField _oldPasswordUser;
    [SerializeField] private TMP_InputField _newPasswordUser1;
    [SerializeField] private TMP_InputField _newPasswordUser2;

    [Header("Error")]
    [SerializeField] private GameObject _errorEntry;

    [Header("Prefabs")]
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _prefabUser;

    [SerializeField] private TMP_Text _userName;

    private int _countErrorPassword = 0;
    private User _currentUser;


    void Start()
    {
        SaveToJSON.Start();
        CreateUserView.StartCreate(_prefabUser, _parent);
        _mainEntry.onClick.AddListener(MainEntry);
        _adminChangePassword.onClick.AddListener(AdminChangePassword);
        _adminViewUsers.onClick.AddListener(AdminViewUsers);
        _adminAddUsers.onClick.AddListener(AdminAddUsers);
        _adminClose.onClick.AddListener(AdminClose);
        _adminChangePasswordComplete.onClick.AddListener(AdminChangePasswordComplete);
        _adminChangePasswordClose.onClick.AddListener(AdminChangePasswordClose);
        _adminViewUsersClose.onClick.AddListener(AdminViewUsersClose);
        _adminAddUsersComlete.onClick.AddListener(AdminAddUsersComlete);
        _adminAddUsersClose.onClick.AddListener(AdminAddUsersClose);
        _userChangePassword.onClick.AddListener(UserChangePassword);
        _userChangePasswordClose.onClick.AddListener(UserChangePasswordClose);
        _userChangePasswordComplete.onClick.AddListener(UserChangePasswordComlete);
        _userClose.onClick.AddListener(UserClose);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            SaveToJSON.ResetSettings();
        }
    }

    #region MethodsButton

    private void MainEntry()
    {
        if(CheckUsers.CheckAdmin(_login.text, _password.text))
        {
            _currentUser = CheckUsers.ReturnUser(_login.text, _password.text);

            if(_password.text == "1234")
            {
                _errorEntry.SetActive(false);
                _entryPanel.SetActive(false);
                _changePasswordAdminPanel.SetActive(true);
            }
            else
            {
                _errorEntry.SetActive(false);
                _entryPanel.SetActive(false);
                _adminPanel.SetActive(true);
            }



            _countErrorPassword = 0;
        }
        else if(CheckUsers.CheckUser(_login.text, _password.text))
        {
            _currentUser = CheckUsers.ReturnUser(_login.text, _password.text);

            if(_password.text == "")
            {
                _errorEntry.SetActive(false);
                _entryPanel.SetActive(false);
                _changePasswordUserPanel.SetActive(true);
                _oldPasswordUser.gameObject.SetActive(false);
                _userChangePasswordClose.onClick.RemoveAllListeners();
                _userChangePasswordClose.onClick.AddListener(UserChangePasswordCloseToEntryMain);
            }
            else if(CheckUsers.CheckPassword(_currentUser))
            {
                _errorEntry.SetActive(false);
                _entryPanel.SetActive(false);
                _userPanel.SetActive(true);
                _userName.text = _login.text;
            }
            else
            {
                _errorEntry.SetActive(false);
                _entryPanel.SetActive(false);
                _changePasswordUserPanel.SetActive(true);

                _userChangePasswordClose.onClick.RemoveAllListeners();
                _userChangePasswordClose.onClick.AddListener(UserChangePasswordCloseToEntryMain);

                _userName.text = _login.text;
            }

            _countErrorPassword = 0;
        }
        else
        {
            _errorEntry.SetActive(true);
            _countErrorPassword++;
            if (_countErrorPassword >= 3)
            {
                Debug.Log("Выход из-за не верного пароля");
                Application.Quit();
            }
        }

        ClearInputs();
    }
    private void AdminChangePassword()
    {
        _adminPanel.SetActive(false);
        _changePasswordAdminPanel.SetActive(true);
    }
    private void AdminViewUsers()
    {
        _adminPanel.SetActive(false);
        _viewUserPanel.SetActive(true);
    }
    private void AdminAddUsers()
    {
        _adminPanel.SetActive(false);
        _addUserPanel.SetActive(true);
    }
    private void AdminClose()
    {
        _adminPanel.SetActive(false);
        _entryPanel.SetActive(true);
    }
    private void AdminChangePasswordComplete()
    {
        if (!ChangePassword.Change(_oldPasswordAdmin.text, _newPasswordAdmin1.text, _newPasswordAdmin2.text, 0))
        {
            ClearInputs();
            return;
        }

        _changePasswordAdminPanel.SetActive(false);
        _adminPanel.SetActive(true);

        SceneManager.LoadScene(1);
    }
    private void AdminChangePasswordClose()
    {
        _changePasswordAdminPanel.SetActive(false);
        _adminPanel.SetActive(true);
    }
    private void AdminViewUsersClose()
    {
        _viewUserPanel.SetActive(false);
        _adminPanel.SetActive(true);
    }
    private void AdminAddUsersComlete()
    {
        if(AddUser.Add(_newUser.text))
        {
            Debug.Log("Получилось добавить пользователя");
            CreateUserView.CreateAddUser(_newUser.text, _prefabUser, _parent);
            _addUserPanel.SetActive(false);
            _adminPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Произошла ошибка");
        }

        ClearInputs();
    }
    private void AdminAddUsersClose()
    {
        _addUserPanel.SetActive(false);
        _adminPanel.SetActive(true);

        ClearInputs();
    }
    private void UserChangePassword()
    {
        _userPanel.SetActive(false);
        _changePasswordUserPanel.SetActive(true);
    }
    private void UserChangePasswordClose()
    {
        _userPanel.SetActive(true);
        _changePasswordUserPanel.SetActive(false);
    }
    private void UserChangePasswordCloseToEntryMain()
    {
        _changePasswordUserPanel.SetActive(false);
        _entryPanel.SetActive(true);
    }
    private void UserChangePasswordComlete()
    {
        if (!ChangePassword.Change(_oldPasswordUser.text, _newPasswordUser1.text, _newPasswordUser2.text, _currentUser.ID))
        {
            ClearInputs();
            return;
        }

        _userChangePasswordClose.onClick.RemoveAllListeners();
        _userChangePasswordClose.onClick.AddListener(UserChangePasswordClose);

        _userPanel.SetActive(true);
        _changePasswordUserPanel.SetActive(false);

        SceneManager.LoadScene(1);
    }
    private void UserClose()
    {
        _userPanel.SetActive(false);
        _entryPanel.SetActive(true);
    }
    private void ClearInputs()
    {
        _login.text = "";
        _password.text = "";
        _newPasswordUser1.text = "";
        _newPasswordUser2.text = "";
        _newPasswordAdmin1.text = "";
        _newPasswordAdmin2.text = "";
        _oldPasswordAdmin.text = "";
        _oldPasswordUser.text = "";
        _newUser.text = "";
    }

    #endregion
}
