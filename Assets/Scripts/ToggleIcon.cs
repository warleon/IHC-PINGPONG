using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle;
    public Image iconImage;
    public AudioSource musicSource;   // <-- NUEVO

    [Header("Sprites")]
    public Sprite soundOnSprite;      // parlante azul
    public Sprite soundOffSprite;     // parlante rojo (mute)

    private void Awake()
    {
        // Obtener componentes si no están asignados
        if (toggle == null)
            toggle = GetComponent<Toggle>();
        if (iconImage == null)
            iconImage = GetComponent<Image>();

        if (musicSource != null)
            musicSource.loop = true;  // asegurar loop por código

        if (toggle != null)
        {
            // Escuchar cambios del toggle
            toggle.onValueChanged.AddListener(OnToggleChanged);

            // Aplicar estado inicial (incluye icono + música)
            OnToggleChanged(toggle.isOn);
        }
    }

    private void OnDestroy()
    {
        if (toggle != null)
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        // Cambiar icono
        if (iconImage != null)
            iconImage.sprite = isOn ? soundOnSprite : soundOffSprite;

        // Controlar música
        if (musicSource == null) return;

        if (isOn)
        {
            // SONIDO ACTIVADO
            if (!musicSource.isPlaying)
                musicSource.Play();
            else
                musicSource.UnPause();
        }
        else
        {
            // SONIDO MUTEADO
            musicSource.Pause();
        }
    }
}
