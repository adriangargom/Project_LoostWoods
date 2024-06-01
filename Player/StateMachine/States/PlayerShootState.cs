using UnityEngine;

public class PlayerShootState : BaseState
{
    private PlayerFiniteStateMachine _playerStateMachine;
    private readonly InputManager _inputManager;
    private readonly ObstacleDetection _obstacleDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;
    private readonly BaseStatsManager _statsManager;

    private float _reloadCounter;
    private const float MinimumReloadTime = .5f;



    public PlayerShootState(FiniteStateMachine playerFiniteStateMachine) 
        : base(playerFiniteStateMachine)
        {
            _playerStateMachine = playerFiniteStateMachine as PlayerFiniteStateMachine;

            _inputManager = _playerStateMachine.PlayerController.InputManager;
            _obstacleDetection = _playerStateMachine.PlayerController.ObstacleDetection;
            _animator = _playerStateMachine.PlayerController.Animator;
            _healthSystem = _playerStateMachine.PlayerController.HealthSystem;

            _statsManager = _playerStateMachine.PlayerController.BaseStatsManager;
        }




    public override void StateStart()
    {
        _reloadCounter = 0;

        _animator.SetBool("shooting", true);
    }

    public override void StateUpdate()
    {
        _reloadCounter += Time.deltaTime;

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

        // Change to PREVIOUS State
        if(!_inputManager.IsAiming) {
            ChangeToPrevious();
        }
    }

    public override void StateFixedUpdate()
    {
        HandleShootRotation();
    }

    public override void StateExit()
    {
        if(_reloadCounter > MinimumReloadTime)
        {
            HandleProjectileShoot();
        }
        
        _animator.SetBool("shooting", false);
    }


    
    public void HandleShootRotation() {
        Transform cameraTargetPoint = _inputManager.CameraTargetPoint;
        Transform playerBody = _inputManager.PlayerBody;

        Vector3 forward = cameraTargetPoint.forward * _inputManager.RawMovementInput.y;
        Vector3 right = cameraTargetPoint.right * _inputManager.RawMovementInput.x;
        Vector3 orientation = forward + right;
        playerBody.forward = Vector3.Slerp(playerBody.forward, orientation, 0.3f);
    }

    private void HandleProjectileShoot() {
        GameObject actualArrowProjectile = PreparePoolItem();
        if(actualArrowProjectile == null) return;

        Rigidbody arrowProjectileRb = actualArrowProjectile.GetComponent<Rigidbody>();
        float projectileForce = _statsManager.ActualStats[StatsEnum.ProjectileForce];
        arrowProjectileRb.velocity = _inputManager.PlayerBody.forward * projectileForce;

        WeaponSystem projectileWeaponSystem = actualArrowProjectile.GetComponent<WeaponSystem>();
        projectileWeaponSystem.SetDamage(_statsManager.ActualStats[StatsEnum.LongRangeDamage]);

        ItemsPoolManager.Instance.LaunchDelayedItemStore(
            ItemsPoolManager.Instance.ArrowProjectilesPool,
            actualArrowProjectile,
            .8f
        );
    }

    private GameObject PreparePoolItem() {
        GameObject poolObject;

        try
        {
            poolObject = ItemsPoolManager.Instance.ArrowProjectilesPool.GetItem();
        }
        catch (EmptyPoolException)
        {
            return null;
        }

        poolObject.SetActive(true);
        poolObject.transform.position = _inputManager.PlayerBody.position;
        poolObject.transform.forward = _inputManager.PlayerBody.right;

        return poolObject;
    }

}