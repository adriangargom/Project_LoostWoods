using UnityEngine;

public class PlayerMovementState : BaseState
{
    private PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    private readonly BaseStatsManager _statsManager;

    private const float RotationVelocity = .2f;


    public PlayerMovementState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _obstacleDetection = _playerStateMachine.PlayerController.ObstacleDetection;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;
            _statsManager = _playerStateMachine.PlayerController.BaseStatsManager;
        }


    public override void StateStart()
    {
        _animator.SetBool("moving", true);
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
        if(!_obstacleDetection.IsGrounded) {
            ChangeTo<PlayerFallState>();
            return;
        }

        // Change to IDLE State
        if(_inputManager.MovementInput == Vector2.zero) {
            ChangeTo<PlayerIdleState>();
            return;
        }

        // Change to ROLL State
        if(_inputManager.IsRolling) {
            ChangeTo<PlayerRollState>();
            return;
        }

        // Change to OBSTACLE_JUMP State
        if(_obstacleDetection.LowerObstacleCheck && !_obstacleDetection.MediumObstacleCheck) {
            ChangeTo<PlayerObstacleJumpState>();
            return;
        }

        // Change to ATTACK State
        if(_inputManager.IsAttacking) {
            ChangeTo<PlayerAttackState>();
            return;
        }

        // Change to SHOOT State
        if(_inputManager.IsAiming) {
            ChangeTo<PlayerShootState>();
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        HandlePlayerMovement();
    }

    public override void StateExit()
    {
        _animator.SetBool("moving", false);
    }


    private void HandlePlayerMovement()
    {
        Transform playerBody = _inputManager.PlayerBody;
        Transform cameraTargetPoit = _inputManager.CameraTargetPoint;
        Rigidbody rigidbody = _inputManager.PlayerRigidbody;

        float speed = _statsManager.ActualStats[StatsEnum.Speed];

        Vector3 forward = cameraTargetPoit.forward * _inputManager.RawMovementInput.y;
        Vector3 right = cameraTargetPoit.right * _inputManager.RawMovementInput.x;
        Vector3 orientation = forward + right;
        playerBody.forward = Vector3.Slerp(playerBody.forward, orientation, RotationVelocity);

        Vector3 velocity = playerBody.forward * speed * Time.deltaTime * _inputManager.MovementInput.magnitude;
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }
}
