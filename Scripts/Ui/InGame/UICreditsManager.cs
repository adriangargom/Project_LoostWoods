using TMPro;
using UnityEngine;

public class UICreditsManager : MonoBehaviour, IObserver
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStatsManager _playerStatsManager;
    [SerializeField] private TextMeshProUGUI _creditsLabel;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerStatsManager = _playerController.BaseStatsManager as PlayerStatsManager;
        _playerStatsManager.Attach(this);
        _playerStatsManager.Notify();
    }

    public void ObserverUpdate()
    {
        PlayerStatsManager playerStatsManager = _playerController.BaseStatsManager as PlayerStatsManager;
     
        int previousCredits = int.Parse(_creditsLabel.text);
        float actualCredits = playerStatsManager.ActualStats[StatsEnum.Credits];

        LeanTween.value(previousCredits, actualCredits, .5f)
            .setEase(LeanTweenType.linear)
            .setOnUpdate((float value) => {
                _creditsLabel.text = string.Format("{0}", (int)value);
            });
    }
}
