

using UnityEngine;

public class SpikeIdleState : BaseState
{
    private readonly SpikeFiniteStateMachine _spikeStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;


    public SpikeIdleState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _spikeStateMachine = finiteStateMachine as SpikeFiniteStateMachine;

            _enviromentDetection = _spikeStateMachine.SpikeController.EnviromentDetection;
            _animator = _spikeStateMachine.SpikeController.Animator;
            _healthSystem = _spikeStateMachine.SpikeController.HealthSystem;
        }

    public override void StateStart()
    {
        _animator.SetBool("idle", true);
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<SpikeDieState>();
            return;
        }

        if(_enviromentDetection.PlayerDetectionCheck)
        {
            int attackOptions = Random.Range(0, 2);

            switch(attackOptions)
            {
                case 0:
                    ChangeTo<SpikeFollowState>();
                    break;

                case 1:
                    ChangeTo<SpikeLongRangeAttackState>();
                    break;
            }
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("idle", false);
    }
}