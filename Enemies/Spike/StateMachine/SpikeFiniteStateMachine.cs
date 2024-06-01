using System.Collections.Generic;

public class SpikeFiniteStateMachine: FiniteStateMachine
{
    public EnemyController SpikeController { get; private set; }

    public SpikeFiniteStateMachine(EnemyController enemyController)
    {
        SpikeController = enemyController;
        InitializeStateMachineStates();
    }

    protected override void InitializeStateMachineStates()
    {
        BaseState defaultState = new SpikeIdleState(this);

        List<BaseState> states = new() {
            defaultState,
            new SpikeFollowState(this),
            new SpikeMeleeAttackState(this),
            new SpikeLongRangeAttackState(this),
            new SpikeDieState(this)
        };

        SetStates(states, defaultState);
    }
}