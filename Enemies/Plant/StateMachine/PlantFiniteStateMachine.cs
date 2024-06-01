using System.Collections.Generic;

public class PlantFiniteStateMachine: FiniteStateMachine
{
    public EnemyController PlantController { get; private set; }

    public PlantFiniteStateMachine(EnemyController plantController)
    {
        PlantController = plantController;
        InitializeStateMachineStates();
    }

    protected override void InitializeStateMachineStates()
    {
        BaseState defaultState = new PlantIdleState(this);

        List<BaseState> states = new() {
            defaultState,
            new PlantDetectionState(this),
            new PlantMeleAttackState(this),
            new PlantLongRangeAttackState(this),
            new PlantDieState(this)
        };

        SetStates(states, defaultState);
    }
}