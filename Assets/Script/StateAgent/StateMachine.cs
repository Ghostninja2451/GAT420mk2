using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    Dictionary<State, List<KeyValuePair<Transition, State>>> stateTransition = new Dictionary<State, List<KeyValuePair<Transition, State>>>();

    State currentState;

    public void Update()
    {
        if (currentState == null) return;

        //check state transition
        var transitions = stateTransition[currentState];
        foreach(var transition in transitions)
        {
            if(transition.Key.ToTransition())
            {
                // set new State
                SetState(transition.Value);
                break;
            }
        }

        //update state
        currentState.OnUpdate();
    }

    public void SetState(State newState)
    {
        currentState.OnExit();
        currentState = newState;
        newState.OnEnter();
    }
}
