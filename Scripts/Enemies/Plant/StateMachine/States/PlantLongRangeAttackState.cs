using UnityEngine;

public class PlantLongRangeAttackState : BaseState
{
    private readonly PlantFiniteStateMachine _plantStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly BaseStatsManager _statsManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    
    private readonly ItemsPoolManager _itemsPoolManager;
    private float _attackDelay;
    private readonly float _rotationVelocity = .2f;



    public PlantLongRangeAttackState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _plantStateMachine = finiteStateMachine as PlantFiniteStateMachine;

            _enviromentDetection = _plantStateMachine.PlantController.EnviromentDetection;
            _statsManager = _plantStateMachine.PlantController.StatsManager;
            _animator = _plantStateMachine.PlantController.Animator;
            _healthSystem = _plantStateMachine.PlantController.HealthSystem;

            _itemsPoolManager = ItemsPoolManager.Instance;
        }

    public override void StateStart()
    {
        SoundEffectsAudioManager.Instance.PlaySoundEffect(
            SoundEffectsAudioManager.Instance.PlantAttack
        );

        _attackDelay = _statsManager.ActualStats[StatsEnum.LongRangeAttackCooldown];
        _animator.SetTrigger("shoot");

        HandleProjectileShoot();
    }

    public override void StateUpdate()
    {
        UpdateCounter();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<PlantDieState>();
            return;
        }
    }

    public override void StateFixedUpdate() {
        HandleRotation();
    }

    public override void StateExit()
    {
        _animator.ResetTrigger("shoot");
    }



    private void UpdateCounter()
    {
        _attackDelay -= Time.deltaTime;

        if(_attackDelay <= 0)
        {
            ChangeToPrevious();
            return;
        }
    }

    private void HandleProjectileShoot()
    {
        GameObject projectile = _itemsPoolManager.PlantProjectilesPool.GetItem();
        if(projectile == null) return;

        projectile.transform.position = _enviromentDetection.ShootPoint.position;
        projectile.transform.forward = _enviromentDetection.EnemyBody.right;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        float projectileForce = _statsManager.ActualStats[StatsEnum.ProjectileForce];
        projectileRb.velocity = _enviromentDetection.EnemyBody.forward * projectileForce;

        WeaponSystem projectileWeaponSystem = projectile.GetComponent<WeaponSystem>();
        projectileWeaponSystem.SetDamage(_statsManager.ActualStats[StatsEnum.LongRangeDamage]);

        _itemsPoolManager.LaunchDelayedItemStore(_itemsPoolManager.PlantProjectilesPool, projectile, 4f);
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