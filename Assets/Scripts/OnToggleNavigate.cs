using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnToggleNavigate : MonoBehaviour
{
    public Toggle toggle;
    public string sceneName;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
            SceneManager.LoadScene(sceneName);
    }
}
