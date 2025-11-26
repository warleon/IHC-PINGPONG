using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnToggle : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource audioSource;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
            audioSource.Play();
    }
}