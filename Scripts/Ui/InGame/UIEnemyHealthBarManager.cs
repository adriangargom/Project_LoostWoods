

using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBarManager : MonoBehaviour, IObserver
{
    public HealthSystem HealthSystem;
    [SerializeField] private Slider _healthSlider;


    void Start()
    {
        HealthSystem.Attach(this);
        HealthSystem.Notify();
    }

    void Update() {
        transform.LookAt(Camera.main.transform.position);
    }

    public void ObserverUpdate()
    {
        _healthSlider.maxValue = HealthSystem.MaxHealthQuantity;

        LeanTween.value(_healthSlider.value, HealthSystem.ActualHealth, .2f)
            .setEase(LeanTweenType.linear)
            .setOnUpdate((float value) => {
                _healthSlider.value = value;
            });
    }
}