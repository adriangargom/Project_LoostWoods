using UnityEngine;

public class PlantIdleState : BaseState
{
    private readonly PlantFiniteStateMachine _plantStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;


    public PlantIdleState(FiniteStateMachine finiteStateMachine)
        : base(finiteStateMachine)
        {
            _plantStateMachine = finiteStateMachine as PlantFiniteStateMachine;

            _enviromentDetection = _plantStateMachine.PlantController.EnviromentDetection;
            _animator = _plantStateMachine.PlantController.Animator;
            _healthSystem = _plantStateMachine.PlantController.HealthSystem;
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

        // Change to PLANT_DETECTION State
        if(_enviromentDetection.PlayerDetectionCheck)
        {
            ChangeTo<PlantDetectionState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("idle", false);
    }
}