using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayModeManager : MonoBehaviour
{
    [SerializeField] private Slider _roundsSlider;
    [SerializeField] private TextMeshProUGUI _roundsStatusFrame;
    [SerializeField] private int _roundsValue;

    void Start()
    {
        HandleRoundsChange();
    }

    public void HandleRoundsChange()
    {
        int roundsSliderValue = (int) _roundsSlider.value;

        _roundsStatusFrame.text = string.Format("{0}", roundsSliderValue);
        _roundsValue = roundsSliderValue;

        StorageSystem.Instance.SettingsProfile.RoundsQuantityValue = roundsSliderValue;
    }
}
