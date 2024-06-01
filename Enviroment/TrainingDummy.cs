using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string HitOneAnimationName = "Hit_1";
    private const string HitTwoAnimationName = "Hit_2"; 


    void Start() 
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) 
    {
        int randomHitId = Random.Range(0, 2);
        switch(randomHitId) 
        {
            case 0:
                _animator.Play(HitOneAnimationName);
                break;
            case 1:
                _animator.Play(HitTwoAnimationName);
                break;
        }
    }
}
