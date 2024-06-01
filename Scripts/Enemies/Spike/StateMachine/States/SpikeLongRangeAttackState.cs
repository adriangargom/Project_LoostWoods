using UnityEngine;

public class SpikeLongRangeAttackState : BaseState
{
    private readonly SpikeFiniteStateMachine _spikeStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly BaseStatsManager _statsManager;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    
    private readonly ItemsPoolManager _itemsPoolManager;
    private float _attackDelay;
    private readonly float _rotationVelocity = .05f;


    public SpikeLongRangeAttackState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _spikeStateMachine = finiteStateMachine as SpikeFiniteStateMachine;

            _statsManager = _spikeStateMachine.SpikeController.StatsManager;
            _enviromentDetection = _spikeStateMachine.SpikeController.EnviromentDetection;
            _animator = _spikeStateMachine.SpikeController.Animator;
            _healthSystem = _spikeStateMachine.SpikeController.HealthSystem;

            _itemsPoolManager = ItemsPoolManager.Instance;
        }


    public override void StateStart()
    {
        _attackDelay = _statsManager.ActualStats[StatsEnum.LongRangeAttackCooldown];
        _animator.SetTrigger("longRangeAttack");
        
        HandleProjectileShoot();
    }

    public override void StateUpdate()
    {
        UpdateCounter();

        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<SpikeDieState>();
            return;
        }
    }

    public override void StateFixedUpdate()
    {
        HandleRotation();
    }

    public override void StateExit()
    {
        _animator.ResetTrigger("longRangeAttack");
    }


    
    private void UpdateCounter()
    {
        _attackDelay -= Time.deltaTime;

        if(_attackDelay <= 0)
        {
            ChangeTo<SpikeIdleState>();
            return;
        }
    }

    private void HandleProjectileShoot()
    {
        GameObject projectile = _itemsPoolManager.SpikeArrowProjectilesPool.GetItem();
        if(projectile == null) return;

        projectile.transform.position = _enviromentDetection.ShootPoint.position;
        projectile.transform.forward = _enviromentDetection.EnemyBody.right;

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        float projectileForce = _statsManager.ActualStats[StatsEnum.ProjectileForce];
        projectileRb.velocity = _enviromentDetection.EnemyBody.forward * projectileForce;

        WeaponSystem projectileWeaponSystem = projectile.GetComponent<WeaponSystem>();
        projectileWeaponSystem.SetDamage(_statsManager.ActualStats[StatsEnum.LongRangeDamage]);

        _itemsPoolManager.LaunchDelayedItemStore(_itemsPoolManager.SpikeArrowProjectilesPool, projectile, 5f);
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