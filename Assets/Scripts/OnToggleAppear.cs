using UnityEngine;
using UnityEngine.UI;

public class OnToggleAppear : MonoBehaviour
{
    public Toggle toggle;
    public GameObject[] objectsToControl;
    public bool showOnToggleOn = true;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);

        // Initialize visibility based on current toggle value
        UpdateObjects(toggle.isOn);
    }

    void OnToggleChanged(bool isOn)
    {
        UpdateObjects(isOn);
    }

    void UpdateObjects(bool toggleState)
    {
        bool visible = (toggleState == showOnToggleOn);

        foreach (GameObject obj in objectsToControl)
        {
            if (obj != null)
                obj.SetActive(visible);
        }
    }
}
