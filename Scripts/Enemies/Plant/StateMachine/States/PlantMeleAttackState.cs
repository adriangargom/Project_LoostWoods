using UnityEngine;

public class PlantMeleAttackState : BaseState
{
    private readonly PlantFiniteStateMachine _plantStateMachine;

    private readonly EnviromentDetection _enviromentDetection;
    private readonly BaseStatsManager _statsManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;

    private float _attackCooldownTimer;
    private readonly float _rotationVelocity = .1f;


    public PlantMeleAttackState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _plantStateMachine = finiteStateMachine as PlantFiniteStateMachine;

            _enviromentDetection = _plantStateMachine.PlantController.EnviromentDetection;
            _statsManager = _plantStateMachine.PlantController.StatsManager;
            _animator = _plantStateMachine.PlantController.Animator;
            _healthSystem = _plantStateMachine.PlantController.HealthSystem;
        }

    public override void StateStart()
    {
        SoundEffectsAudioManager.Instance.PlaySoundEffect(
            SoundEffectsAudioManager.Instance.PlantAttack
        );

        _attackCooldownTimer = _statsManager.ActualStats[StatsEnum.MeleAttackCooldown];
        _animator.SetTrigger("attack");
    }

    public override void StateUpdate()
    {
        UpdateTimer();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<PlantDieState>();
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        HandleRotation();
    }


    public override void StateExit()
    {
        _animator.ResetTrigger("attack");
    }



    private void UpdateTimer()
    {
        _attackCooldownTimer -= Time.deltaTime;

        if(_attackCooldownTimer <= 0)
        {
            ChangeToPrevious();
        }
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