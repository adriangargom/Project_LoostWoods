using UnityEngine;

public class PlayerFallState : BaseState
{
    private readonly PlayerFiniteStateMachine _playerStateMachine;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    
        
    public PlayerFallState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _obstacleDetection = _playerStateMachine.PlayerController.ObstacleDetection;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;
        }


    public override void StateStart()
    {
        _animator.SetBool("fall", true);
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<PlayerDieState>();
            return;
        }

        // Change to IDLE State
        if(_obstacleDetection.IsGrounded) {
            ChangeTo<PlayerIdleState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("fall", false);
    }
}