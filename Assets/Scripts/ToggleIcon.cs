using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle;
    public Image iconImage;

    [Header("Sprites")]
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Awake()
    {
        // Automatically get components if not assigned
        if (toggle == null)
            toggle = GetComponent<Toggle>();
        if (iconImage == null)
            iconImage = GetComponent<Image>();

        // Listen for toggle changes
        toggle.onValueChanged.AddListener(OnToggleChanged);

        // Set initial icon
        OnToggleChanged(toggle.isOn);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        iconImage.sprite = isOn ? soundOnSprite : soundOffSprite;
    }
}
