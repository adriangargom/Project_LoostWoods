using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private int[] _posibleHealthAmounts;
    [SerializeField] private int _healthIncrement;

    
    void Start()
    {
        _healthIncrement = GenerateRandomPosibleHealthAmount();
        LeanTween.rotateZ(gameObject, 1, 2.5f).setLoopClamp();
    }


    private int GenerateRandomPosibleHealthAmount()
    {
        int randomizedId = Random.Range(0, _posibleHealthAmounts.Length-1);
        return _posibleHealthAmounts[randomizedId];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player") {
            return;
        }

        if(other.gameObject.TryGetComponent(out HealthSystem healthSystem)) {
            Grab(healthSystem);
        }
    }

    
    public void Grab(HealthSystem healthSystem)
    {
        SoundEffectsAudioManager.Instance.PlaySoundEffect(
            SoundEffectsAudioManager.Instance.HealthItemGrab
        );

        healthSystem.IncreaseActualHealth(_healthIncrement);
        ItemsPoolManager.Instance.HealthOrbPool.StoreItem(gameObject);
    }
}
