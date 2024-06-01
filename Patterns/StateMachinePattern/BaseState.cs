
public abstract class BaseState
{
    protected FiniteStateMachine StateMachine;

    public BaseState(FiniteStateMachine finiteStateMachine) {
        StateMachine = finiteStateMachine;
    }
    
    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void StateExit();

    public void ChangeTo<S>() where S: BaseState
    {
        StateMachine.ChangeState<S>();
    }

    public void ChangeToPrevious()
    {
        StateMachine.ChangeToPreviousState();
    }
}