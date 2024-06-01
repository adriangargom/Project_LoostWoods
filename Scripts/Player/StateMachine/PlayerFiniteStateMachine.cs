using System.Collections.Generic;

public class PlayerFiniteStateMachine : FiniteStateMachine
{
    public PlayerController PlayerController { get; private set; }


    public PlayerFiniteStateMachine(PlayerController playerController)
    {
        PlayerController = playerController;
        InitializeStateMachineStates();
    }


    protected override void InitializeStateMachineStates()
    {
        BaseState defaultState = new PlayerIdleState(this);

        List<BaseState> playerStates = new() {
            defaultState,
            new PlayerMovementState(this),
            new PlayerObstacleJumpState(this),
            new PlayerRollState(this),
            new PlayerAttackState(this),
            new PlayerShootState(this),
            new PlayerFallState(this),
            new PlayerDieState(this)
        };

        SetStates(playerStates, defaultState);
    }
}
