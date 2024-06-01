using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarManager : MonoBehaviour, IObserver
{
    private CameraShakeHandler _cameraShakeHandler;

    public HealthSystem HealthSystem;
    [SerializeField] private Slider _healthSlider;


    void Start()
    {
        _cameraShakeHandler = CameraShakeHandler.Instance;
        HealthSystem.Attach(this);
        HealthSystem.Notify();
    }

    public void ObserverUpdate()
    {
        HandleCameraShake();

        _healthSlider.maxValue = HealthSystem.MaxHealthQuantity;

        LeanTween.value(_healthSlider.value, HealthSystem.ActualHealth, .5f)
            .setEase(LeanTweenType.linear)
            .setOnUpdate((float value) => {
                _healthSlider.value = value;
            });
    }

    public void HandleCameraShake()
    {
        if(HealthSystem.CurrentState == HealthSystemStatesEnum.ActualHealthDecrease)
            _cameraShakeHandler.LaunchCameraShake(2, .2f);
    }
}