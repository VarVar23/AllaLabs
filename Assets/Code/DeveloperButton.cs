using UnityEngine;
using UnityEngine.UI;

public class DeveloperButton : MonoBehaviour
{
    [SerializeField] private Button _developerButton;
    [SerializeField] private GameObject _panelStudent;
    private bool _active;

    private void Start()
    {
        _developerButton.onClick.AddListener(DeveloperButtonMethod);
    }

    private void DeveloperButtonMethod()
    {
        if(_active)
        {
            _panelStudent.SetActive(false);
        }
        else
        {
            _panelStudent.SetActive(true);
        }

        _active = !_active;
    }
}
