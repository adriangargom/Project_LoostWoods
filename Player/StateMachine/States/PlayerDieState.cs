using System.Collections;
using UnityEngine;

public class PlayerDieState : BaseState
{
    private readonly PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly Animator _animator;


    public PlayerDieState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _animator = _playerStateMachine.PlayerController.Animator;
        }


    public override void StateStart()
    {
        _animator.SetBool("die", true);
        DieParticles();
    }

    public override void StateUpdate() {}

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("die", false);
    }

    private void DieParticles()
    {
        ParticlesPoolManager particlesPoolManager = ParticlesPoolManager.Instance;

        GameObject dieParticles = particlesPoolManager.PlayerDieParticlesPool.GetItem();
        dieParticles.transform.position = _inputManager.PlayerBody.position;
        
        particlesPoolManager.LaunchDelayedItemStore(
            particlesPoolManager.EnemyDieParticlesPool,
            dieParticles,
            5f
        );
    }
}