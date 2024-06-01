using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class FiniteStateMachine: BaseObservable
{
    // Dictionary that contains all the available states inside of the FSM
    // Stores the Type of the state and the referent instance of this state
    private readonly Dictionary<Type, BaseState> _states = new();

    public BaseState _defaultState { get; private set; }
    public BaseState CurrentState { get; private set; }
    public BaseState PreviousState { get; private set; }


    // Sets all the states and the default state of the state machine
    public void SetStates(List<BaseState> states, BaseState defaultState)
    {
        _states.Clear();

        foreach (var state in states)
        {
            _states.Add(state.GetType(), state);
        }

        _defaultState = defaultState;
        ChangeToDefaultState();
    }


    public void Update() 
    {
        CurrentState.StateUpdate();
    }

    public void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }


    // Changes the actual state to the new provided one and
    // notifies about the new changes to all the Observers
    private void ChangeState(BaseState newState)
    {
        bool hasCurrentState = CurrentState != null;
        if(hasCurrentState)
        {
            PreviousState = CurrentState;
            CurrentState.StateExit();
        }

        CurrentState = newState;
        CurrentState.StateStart();

        Notify();
    }

    // Changes the actual FSM state to the referent state type from the
    // "_states" dictionary based on the provided generic type "S" if 
    // the provided state exists inside of the "_states" dictionary
    public void ChangeState<S>() where S: BaseState
    {
        bool stateExists = _states.Keys.Contains(typeof(S));
        if(!stateExists) {
            return;
        }
        
        ChangeState(_states[typeof(S)]);
    }

    // Changes the actual state from the state machine to the previous one if
    // exists a previous state and the actual state is not equal to the previous one
    public void ChangeToPreviousState()
    {
        bool hasPreviousState = PreviousState != null;
        bool isCurrentStateAsPreviousState = PreviousState == CurrentState;

        if(!hasPreviousState || isCurrentStateAsPreviousState)
        {
            ChangeToDefaultState();
            return;
        }

        ChangeState(PreviousState);
    }

    // Changes the actual FSM to the default state
    public void ChangeToDefaultState()
    {
        ChangeState(_defaultState);
    }

    // Method that initializes the list with all the available states
    protected abstract void InitializeStateMachineStates();
}