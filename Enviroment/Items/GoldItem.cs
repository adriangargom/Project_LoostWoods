using UnityEngine;

public class GoldItem : MonoBehaviour
{
    [SerializeField] private float[] _possibleCreditAmounts;
    [SerializeField] private float _creditsIncrement;


    void Start()
    {
        _creditsIncrement = GenerateRandomPosibleCreditAmount();
        LeanTween.rotateZ(gameObject, 360, 2.5f).setLoopClamp();
    }

    
    private float GenerateRandomPosibleCreditAmount()
    {
        int randomizedId = Random.Range(0, _possibleCreditAmounts.Length-1);
        return _possibleCreditAmounts[randomizedId];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player") {
            return;
        }

        if(other.gameObject.TryGetComponent(out PlayerController playerController)) {
            Grab(playerController);
        }
    }

    public void Grab(PlayerController playerController)
    {
        SoundEffectsAudioManager.Instance.PlaySoundEffect(
            SoundEffectsAudioManager.Instance.GoldItemGrab
        );

        PlayerStatsManager playerStatsManager = playerController.BaseStatsManager as PlayerStatsManager;
        playerStatsManager.ActualStats[StatsEnum.Credits] += (int) _creditsIncrement;
        playerStatsManager.Notify();

        ItemsPoolManager.Instance.GoldPool.StoreItem(gameObject);
        gameObject.SetActive(false);
    }
}
