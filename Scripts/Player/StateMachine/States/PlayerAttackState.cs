using System.Collections;
using UnityEngine;

public class PlayerAttackState: BaseState
{
    private readonly PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    
    private readonly string[] _attacks = {"attackOne", "attackTwo"};
    private float _attackTimer;
    private int _actualAttackIndex = 0;
    private const float _nextAttackCooldown = .4f;



    public PlayerAttackState(FiniteStateMachine playerFiniteStateMachine) 
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
        SoundEffectsAudioManager.Instance.LaunchDelayedSoundEffect(
            SoundEffectsAudioManager.Instance.PlayerAttack, .2f
        );
        
        _inputManager.PlayerRigidbody.velocity = Vector3.zero;
        _attackTimer = .8f;

        _animator.SetTrigger(_attacks[_actualAttackIndex]);
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
        
        // Change to FALL State
        if(!_obstacleDetection.IsGrounded) {
            ChangeTo<PlayerFallState>();
            return;
        }

        // Repeat ATTACK State
        if(_attackTimer <= _nextAttackCooldown && _inputManager.IsAttacking) {
            ChangeTo<PlayerAttackState>();
            return;
        }

        // Change to ROLL State
        if(_attackTimer <= _nextAttackCooldown && _inputManager.IsRolling) {
            ChangeTo<PlayerRollState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _actualAttackIndex = (_actualAttackIndex+1 >= _attacks.Length)? 0 : _actualAttackIndex+1;
        _animator.ResetTrigger(_attacks[_actualAttackIndex]);
    }

    private void UpdateTimer()
    {
        _attackTimer -= Time.deltaTime;

        if(_attackTimer <= 0)
        {
            ChangeTo<PlayerIdleState>();
        }
    }
}