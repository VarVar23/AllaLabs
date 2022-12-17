using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputKey : MonoBehaviour
{
    [SerializeField] private TMP_InputField _keyInput;
    [SerializeField] private Button _buttonEnter;
    [SerializeField] private GameObject _error;

    private void Start()
    {
        _error.SetActive(false);
        _buttonEnter.onClick.AddListener(CheckKey);
    }

    private void CheckKey()
    {
        if(_keyInput.text != Encryptor.Key)
        {
            _error.SetActive(true);
            _keyInput.text = "";
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
