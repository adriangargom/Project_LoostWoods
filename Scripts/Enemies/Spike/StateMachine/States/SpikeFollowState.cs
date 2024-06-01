
using UnityEngine;

public class SpikeFollowState : BaseState
{
    private readonly SpikeFiniteStateMachine _spikeStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    

    public SpikeFollowState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _spikeStateMachine = finiteStateMachine as SpikeFiniteStateMachine;

            _enviromentDetection = _spikeStateMachine.SpikeController.EnviromentDetection;
            _animator = _spikeStateMachine.SpikeController.Animator;
            _healthSystem = _spikeStateMachine.SpikeController.HealthSystem;
        }

    public override void StateStart()
    {
        _animator.SetBool("move", true);
        _enviromentDetection.NavMeshAgent.isStopped = false;
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<SpikeDieState>();
            return;
        }

        // Change to MELEE_ATTACK State
        float meleeAttackRange = _spikeStateMachine.SpikeController.StatsManager
            .ActualStats[StatsEnum.MeleAttackRange];

        if(_enviromentDetection.ActualDistanceToPlayer <= meleeAttackRange)
        {
            ChangeTo<SpikeMeleeAttackState>();
            return;
        }

        // Change to IDLE State
        if(!_enviromentDetection.PlayerDetectionCheck)
        {
            ChangeTo<SpikeIdleState>();
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    public override void StateExit()
    {
        _animator.SetBool("move", false);
        _enviromentDetection.NavMeshAgent.isStopped = true;
    }

    private void HandleRotation()
    {
        Vector3 playerPos = _enviromentDetection.PlayerBody.position;
        Vector3 enemyPos = _enviromentDetection.EnemyBody.position;

        Vector3 direction = new Vector3(playerPos.x - enemyPos.x, 0, playerPos.z - enemyPos.z);

        _enviromentDetection.Enemy.forward = Vector3.Slerp(
            _enviromentDetection.Enemy.forward, 
            direction, 
            .2f
        );
    }

    private void HandleMovement()
    {        
        Vector3 targetPosition = _enviromentDetection.PlayerBody.position;
        _enviromentDetection.NavMeshAgent.SetDestination(targetPosition);
    }
    
}