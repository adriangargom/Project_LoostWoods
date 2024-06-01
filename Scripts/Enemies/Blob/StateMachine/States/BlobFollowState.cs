using UnityEngine;

public class BlobFollowState : BaseState
{
    private readonly BlobFiniteStateMachine _blobStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;


    public BlobFollowState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _blobStateMachine = finiteStateMachine as BlobFiniteStateMachine;

            _enviromentDetection = _blobStateMachine.BlobController.EnviromentDetection;
            _animator = _blobStateMachine.BlobController.Animator;
            _healthSystem = _blobStateMachine.BlobController.HealthSystem;

        }


    public override void StateStart()
    {
        _animator.SetBool("move", true);
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<BlobDieState>();
            return;
        }

        // Change to ATTACK State
        float targetDistance = _blobStateMachine.BlobController.StatsManager
            .ActualStats[StatsEnum.MeleAttackRange];

        if(_enviromentDetection.ActualDistanceToPlayer <= targetDistance)
        {
            ChangeTo<BlobAttackState>();
            return;
        }

        // Change to IDLE State
        if(!_enviromentDetection.PlayerDetectionCheck)
        {
            ChangeTo<BlobIdleState>();
            return;
        }
    }

    public override void StateFixedUpdate() {
        HandleMovement();
    }

    public override void StateExit()
    {
        _animator.SetBool("move", false);
    }

    
    private void HandleMovement()
    {
        Vector3 targetPosition = _enviromentDetection.PlayerBody.position;
        _enviromentDetection.NavMeshAgent.SetDestination(targetPosition);
    }
}