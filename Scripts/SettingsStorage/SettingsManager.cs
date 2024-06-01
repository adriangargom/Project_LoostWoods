using UnityEngine;

public class SettingsManager : MonoBehaviour, IObserver
{
    void Start()
    {
        StorageSystem.Instance.Attach(this);
    }

    // Sound Settings
    private void ApplySoundSettings()
    {
        StorageSystem storageSystem = StorageSystem.Instance;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        int volume = storageSystem.SettingsProfile.Volume;
        bool isMuted = storageSystem.SettingsProfile.IsMuted;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume * 0.01f;
            audioSource.mute = isMuted;
        }
    }

    // Graphical Settings
    private void ApplyGraphicalSettings()
    {
        ApplyQualityLevel();
    }

    private void ApplyQualityLevel()
    {
        StorageSystem storageSystem = StorageSystem.Instance;
        QualitySettings.SetQualityLevel(storageSystem.SettingsProfile.QualityValue); 
    }

    // Screen Settings
    private void ApplyScreenSettings()
    {
        StorageSystem storageSystem = StorageSystem.Instance;

        switch (storageSystem.SettingsProfile.FpsQuantityValue)
        {
            case 1:
                Application.targetFrameRate = 60;
                break;

            case 2:
                Application.targetFrameRate = 90;
                break;

            case 3:
                Application.targetFrameRate = 120;
                break;

            default:
                Application.targetFrameRate = 30;
                break;
        }
    }

    public void ObserverUpdate()
    {
        ApplySoundSettings();
        ApplyGraphicalSettings();
        ApplyScreenSettings();
    }
}
