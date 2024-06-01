
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsScreenManager : MonoBehaviour, IObserver
{
    [SerializeField] private Slider _fpsSlider;


    void Start()
    {
        StorageSystem.Instance.Attach(this);

        HandleFpsChange();
    }

    [SerializeField] private TextMeshProUGUI _fpsStatusFrame;
    [SerializeField] private int _fpsValue;
    private readonly string[] _fpsValueStatusMessages = {"30fps", "60fps", "90fps", "120fps"};
    private readonly int[] _fpsValues = {30, 60, 90, 120};

    public void HandleFpsChange()
    {
        int qualitySliderValue = (int) _fpsSlider.value;

        _fpsStatusFrame.text = _fpsValueStatusMessages[qualitySliderValue];        
        _fpsValue = qualitySliderValue;

        StorageSystem.Instance.SettingsProfile.FpsQuantityValue = qualitySliderValue;
    }


    // IObserver
    public void ObserverUpdate()
    {
        StorageSystem storageSystemInstance = StorageSystem.Instance;
        _fpsValue = storageSystemInstance.SettingsProfile.FpsQuantityValue;
        _fpsSlider.value = _fpsValue;

        Application.targetFrameRate = _fpsValues[storageSystemInstance.SettingsProfile.FpsQuantityValue];
    }
}