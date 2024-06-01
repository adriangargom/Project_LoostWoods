

using System;
using UnityEngine;

public class SpikeMeleeAttackState : BaseState
{
    private readonly SpikeFiniteStateMachine _spikeStateMachine;
    private readonly BaseStatsManager _statsManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    
    private float _attackCooldownTimer;


    public SpikeMeleeAttackState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _spikeStateMachine = finiteStateMachine as SpikeFiniteStateMachine;

            _statsManager = _spikeStateMachine.SpikeController.StatsManager;
            _animator = _spikeStateMachine.SpikeController.Animator;
            _healthSystem = _spikeStateMachine.SpikeController.HealthSystem;
        }

    public override void StateStart()
    {
        SoundEffectsAudioManager.Instance.LaunchDelayedSoundEffect(
            SoundEffectsAudioManager.Instance.SpikeMeleeAttack, .2f
        );

        _attackCooldownTimer = _statsManager.ActualStats[StatsEnum.MeleAttackCooldown];
        _animator.SetTrigger("meleeAttack");
    }


    public override void StateUpdate()
    {
        UpdateTimer();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<SpikeDieState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}


    public override void StateExit()
    {
        _animator.ResetTrigger("meleeAttack");
    }


    private void UpdateTimer()
    {
        _attackCooldownTimer -= Time.deltaTime;

        if(_attackCooldownTimer <= 0)
        {
            ChangeTo<SpikeIdleState>();
        }
    }
}