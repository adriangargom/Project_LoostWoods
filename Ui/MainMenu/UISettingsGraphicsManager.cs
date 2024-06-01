using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsGraphicsManager : MonoBehaviour, IObserver
{
    [SerializeField] private Slider _qualitySlider;
    [SerializeField] private TextMeshProUGUI _qualityStatusFrame;
    [SerializeField] private int _qualityValue;
    private readonly string[] _qualityValueStatusMessages = {"Potato", "Medium", "High"};

    
    void Start()
    {
        StorageSystem.Instance.Attach(this);

        HandleQualityChange();
    }


    public void HandleQualityChange()
    {
        int qualitySliderValue = (int) _qualitySlider.value;

        _qualityStatusFrame.text = _qualityValueStatusMessages[qualitySliderValue];        
        _qualityValue = qualitySliderValue;

        StorageSystem.Instance.SettingsProfile.QualityValue = qualitySliderValue;
    }

    // IObserver
    public void ObserverUpdate()
    {
        StorageSystem storageSystemInstance = StorageSystem.Instance;

        _qualityValue = storageSystemInstance.SettingsProfile.QualityValue;
        _qualitySlider.value = _qualityValue;
    }
}
