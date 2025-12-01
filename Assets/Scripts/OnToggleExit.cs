using UnityEngine;
using UnityEngine.UI;

public class OnToggleExit : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            // Quit the application
            Application.Quit();

#if UNITY_EDITOR
            // Stop play mode when testing in the editor
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
