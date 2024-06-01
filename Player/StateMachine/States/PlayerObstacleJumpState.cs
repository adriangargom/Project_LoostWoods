using UnityEngine;

public class PlayerObstacleJumpState : BaseState
{
    private PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    private readonly Transform _player;

    private float _obstacleJumpTimer;
    private Vector3 _landPosition;

    private const float StopDistance = .1f;
    private const float LandDistance = .3f;



    public PlayerObstacleJumpState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _obstacleDetection = _playerStateMachine.PlayerController.ObstacleDetection;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;

            _player = _inputManager.Player;
        }



    public override void StateStart()
    {
        if(!_obstacleDetection.LowerObstacleRaycastHit.collider.CompareTag("canJump"))
        {
            ChangeToPrevious();
            return;
        }

        CalculateLandingPosition();

        _obstacleJumpTimer = 4f;
        DisablePhysics();
        _animator.SetTrigger("obstacleJump");
    }

    public override void StateUpdate()
    {
        _obstacleJumpTimer -= Time.deltaTime;

        bool IsPlayerCloseToTargetPosition = Vector3.Distance(_player.position, _landPosition) <= StopDistance;
        bool IsTimerFinished = _obstacleJumpTimer <= 0;

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<PlayerDieState>();
            return;
        }

        // Change to PREVIOUS State
        if(IsPlayerCloseToTargetPosition || IsTimerFinished) 
        {
            ChangeToPrevious();
        }
    }

    public override void StateFixedUpdate()
    {
        _player.position = Vector3.Lerp(_player.position, _landPosition, 0.05f);
    }

    public override void StateExit()
    {    
        EnablePhysics();
        _animator.ResetTrigger("obstacleJump");
    }



    private void CalculateLandingPosition() {
        Transform playerBody = _inputManager.PlayerBody;

        Vector3 forwardLandingPos = _obstacleDetection.LowerObstacleRaycastHit.point + playerBody.forward * LandDistance;
        _landPosition = new Vector3(forwardLandingPos.x, forwardLandingPos.y + _player.lossyScale.y, forwardLandingPos.z);
    }

    private void EnablePhysics()
    {
        _inputManager.PlayerCollider.enabled = true;
        _inputManager.PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void DisablePhysics()
    {
        _inputManager.PlayerCollider.enabled = false;
        _inputManager.PlayerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
