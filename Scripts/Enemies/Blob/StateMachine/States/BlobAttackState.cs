using UnityEngine;

public class BlobAttackState : BaseState
{
    private readonly BlobFiniteStateMachine _blobStateMachine;
    private readonly BaseStatsManager _statsManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;

    private float _attackCooldownTimer;


    public BlobAttackState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _blobStateMachine = finiteStateMachine as BlobFiniteStateMachine;

            _statsManager = _blobStateMachine.BlobController.StatsManager;
            _animator = _blobStateMachine.BlobController.Animator;
            _healthSystem = _blobStateMachine.BlobController.HealthSystem;
        }


    public override void StateStart()
    {
        SoundEffectsAudioManager.Instance.LaunchDelayedSoundEffect(
            SoundEffectsAudioManager.Instance.BlobAttack, .8f
        );

        _attackCooldownTimer = _statsManager.ActualStats[StatsEnum.MeleAttackCooldown];
        _animator.SetTrigger("attack");
    }

    public override void StateUpdate()
    {
        UpdateTimer();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<BlobDieState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.ResetTrigger("attack");
    }

    
    // Updates the attack cooldown timer by deltaTime each frame and when arrives to zero
    // Changes to the previous state in the state machine 
    private void UpdateTimer()
    {
        _attackCooldownTimer -= Time.deltaTime;

        if(_attackCooldownTimer <= 0)
        {
            ChangeToPrevious();
        }
    }
}