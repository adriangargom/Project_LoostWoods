using UnityEngine;

public class SpikeDieState : BaseState
{
    private readonly SpikeFiniteStateMachine _spikeStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly Transform _enemy;


    public SpikeDieState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _spikeStateMachine = finiteStateMachine as SpikeFiniteStateMachine;

            _enviromentDetection = _spikeStateMachine.SpikeController.EnviromentDetection;
            _animator = _spikeStateMachine.SpikeController.Animator;
            _enemy = _spikeStateMachine.SpikeController.EnviromentDetection.Enemy;
        }

    public override void StateStart()
    {
        _animator.SetBool("die", true);
        
        InstantiateDeadItems();
        DieParticles();
    }

    public override void StateUpdate() {}

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("die", false);
    }



    private void InstantiateDeadItems()
    {
        GameObject goldInstance = ItemsPoolManager.Instance.GoldPool.GetItem();
        goldInstance.transform.position = _enviromentDetection.EnemyBody.position;
        goldInstance.SetActive(true);
    }

    private void DieParticles()
    {
        ParticlesPoolManager particlesPoolManager = ParticlesPoolManager.Instance;

        GameObject dieParticles = particlesPoolManager.EnemyDieParticlesPool.GetItem();
        dieParticles.transform.position = _enemy.position;

        particlesPoolManager.LaunchDelayedItemStore(
            particlesPoolManager.EnemyDieParticlesPool,
            dieParticles,
            5f
        );
    }
}