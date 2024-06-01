using UnityEngine;

public class PlayerRollState : BaseState
{
    private PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    private readonly BaseStatsManager _statsManager;

    private float _rollTimer;
    private const float _rotationVelocity = 0.1f;



    public PlayerRollState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;
            _statsManager = _playerStateMachine.PlayerController.BaseStatsManager;
        }


    public override void StateStart()
    {
        _rollTimer = _animator.GetCurrentAnimatorStateInfo(1).length - 0.1f;
        _animator.SetTrigger("roll");
    }

    public override void StateUpdate()
    {
        UpdateTimer();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<PlayerDieState>();
            return;
        }

    }

    public override void StateFixedUpdate()
    {
        HandleRollMovement();
    }

    public override void StateExit()
    {
        _inputManager.PlayerRigidbody.velocity = Vector3.zero;
        _animator.ResetTrigger("roll");
    }


    
    private void UpdateTimer() {
        _rollTimer -= Time.deltaTime;

        if(_rollTimer <= 0) {
            ChangeTo<PlayerIdleState>();
        }
    }
    
    private void HandleRollMovement() {
        Rigidbody playerRb = _inputManager.PlayerRigidbody;
        Transform cameraTargetPoint = _inputManager.CameraTargetPoint;
        Transform playerBody = _inputManager.PlayerBody;

        Vector3 forward = cameraTargetPoint.forward * _inputManager.RawMovementInput.y;
        Vector3 right = cameraTargetPoint.right * _inputManager.RawMovementInput.x;
        Vector3 orientation = forward + right;
        playerBody.forward = Vector3.Slerp(playerBody.forward, orientation, _rotationVelocity);

        float rollSpeed = _statsManager.ActualStats[StatsEnum.RollSpeed];
        float drag = _rollTimer + _rollTimer / 2;

        Vector3 velocity = playerBody.forward * rollSpeed * drag * Time.deltaTime;
        playerRb.velocity = new Vector3(velocity.x, playerRb.velocity.y, velocity.z);
    }
}
