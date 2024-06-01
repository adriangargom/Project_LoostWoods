using UnityEngine;

public class PlayerIdleState : BaseState
{
    private PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;


    public PlayerIdleState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _obstacleDetection = _playerStateMachine.PlayerController.ObstacleDetection;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;
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
            ChangeTo<PlayerDieState>();
            return;
        }

        // Change to FALL State
        if(!_obstacleDetection.IsGrounded)
        {
            ChangeTo<PlayerFallState>();
            return;
        }

        // Change to MOVEMENT State
        if(_inputManager.MovementInput.magnitude > 0)
        {
            ChangeTo<PlayerMovementState>();
            return;
        }

        // Change to ROLL State
        if(_inputManager.IsRolling)
        {
            ChangeTo<PlayerRollState>();
            return;
        }

        // Change to OBSTACLE_JUMP State
        if(_obstacleDetection.LowerObstacleCheck && !_obstacleDetection.MediumObstacleCheck)
        {
            ChangeTo<PlayerObstacleJumpState>();
            return;
        }

        // Change to ATTACK State
        if(_inputManager.IsAttacking)
        {
            ChangeTo<PlayerAttackState>();
            return;
        }

        // Change to SHOOT State
        if(_inputManager.IsAiming)
        {
            ChangeTo<PlayerShootState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("idle", false);
    }
}
