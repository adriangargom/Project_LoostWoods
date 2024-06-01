using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsVolumeManager : MonoBehaviour, IObserver
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumePercentageFrame;
    [SerializeField] private float _volumeQuantity;

    [SerializeField] private Toggle _muteVolumeToggle;
    [SerializeField] private bool _isAudioEnabled;


    void Start()
    {
        StorageSystem.Instance.Attach(this);

        HandleVolumeChange();
        HandleVolumeMute();
    }

    
    public void HandleVolumeChange()
    {
        int volumeSliderValue = (int) _volumeSlider.value;

        _volumePercentageFrame.text = string.Format("{0}%", volumeSliderValue);
        _volumeQuantity = volumeSliderValue;

        StorageSystem.Instance.SettingsProfile.Volume = volumeSliderValue;
    }

    public void HandleVolumeMute()
    {
        _isAudioEnabled = !_muteVolumeToggle.isOn;
        StorageSystem.Instance.SettingsProfile.IsMuted = _muteVolumeToggle.isOn;
    }


    // IObserver
    public void ObserverUpdate()
    {
        StorageSystem storageSystemInstance = StorageSystem.Instance;

        _volumeQuantity = storageSystemInstance.SettingsProfile.Volume;
        _volumeSlider.value = _volumeQuantity;

        _isAudioEnabled = storageSystemInstance.SettingsProfile.IsMuted;
        _muteVolumeToggle.isOn = _isAudioEnabled;
    }
}