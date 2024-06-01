using System.Collections.Generic;

public class BlobFiniteStateMachine: FiniteStateMachine
{
    public EnemyController BlobController { get; private set; }

    public BlobFiniteStateMachine(EnemyController blobController)
    {
        BlobController = blobController;
        InitializeStateMachineStates();
    }

    protected override void InitializeStateMachineStates() 
    {
        BaseState defaultState = new BlobIdleState(this);

        List<BaseState> states = new() {
            defaultState,
            new BlobFollowState(this),
            new BlobAttackState(this),
            new BlobDieState(this)
        };

        SetStates(states, defaultState);
    }
}