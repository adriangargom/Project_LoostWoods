using UnityEngine;

public class PlantDetectionState : BaseState
{
    private readonly PlantFiniteStateMachine _plantStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;

    private readonly float _meleAttackRange;
    private readonly float _rotationVelocity = .2f;


    public PlantDetectionState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _plantStateMachine = finiteStateMachine as PlantFiniteStateMachine;

            _enviromentDetection = _plantStateMachine.PlantController.EnviromentDetection;
            _animator = _plantStateMachine.PlantController.Animator;
            _healthSystem = _plantStateMachine.PlantController.HealthSystem;

            _meleAttackRange = _plantStateMachine.PlantController.StatsManager
                .ActualStats[StatsEnum.MeleAttackRange];

        }

    public override void StateStart()
    {
        _animator.SetBool("idle", true);
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0){
            ChangeTo<PlantDieState>();
            return;
        }

        // Change to MELE_ATTACK State
        if(_enviromentDetection.ActualDistanceToPlayer <= _meleAttackRange)
        {
            ChangeTo<PlantMeleAttackState>();
            return;
        }

        // Change to LONG_RANGE_ATTACK State
        if(_enviromentDetection.PlayerDetectionCheck)
        {
            ChangeTo<PlantLongRangeAttackState>();
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        HandleRotation();
    }

    public override void StateExit()
    {
        _animator.SetBool("idle", false);
    }


    private void HandleRotation()
    {
        Vector3 playerPos = _enviromentDetection.PlayerBody.position;
        Vector3 enemyPos = _enviromentDetection.EnemyBody.position;

        Vector3 direction = new Vector3(playerPos.x - enemyPos.x, 0, playerPos.z - enemyPos.z);

        _enviromentDetection.Enemy.forward = Vector3.Slerp(
            _enviromentDetection.Enemy.forward, 
            direction, 
            _rotationVelocity
        );
    }
}