using UnityEngine;

public class PlantDieState : BaseState
{
    private readonly PlantFiniteStateMachine _plantStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly Transform _enemy;



    public PlantDieState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _plantStateMachine = finiteStateMachine as PlantFiniteStateMachine;

            _enviromentDetection = _plantStateMachine.PlantController.EnviromentDetection;
            _animator = _plantStateMachine.PlantController.Animator;
            _enemy = _plantStateMachine.PlantController.EnviromentDetection.Enemy;
        }



    public override void StateStart()
    {
        SoundEffectsAudioManager.Instance.LaunchDelayedSoundEffect(
            SoundEffectsAudioManager.Instance.Die, .2f
        );

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