using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class State : ScriptableObject
{
    public StateAction[] stateOnExecute;
    public StateAction[] stateOnEnter;
    public StateAction[] stateOnExit;

    public List<Transition> transitions = new List<Transition>();
    
    public void OnEnter(StateManager state)
    {
        Tick(state, stateOnEnter);
    }

    public void OnExecute(StateManager state)
    {
        Tick(state, stateOnExecute);
    }

    public void OnExit(StateManager state)
    {
        Tick(state, stateOnExit);
    }

    public void CheckTransition(StateManager state)
    {
        for (int index = 0; index < transitions.Count; index++)
        {
            if(transitions[index].disable)
            {
                continue;
            }

            if(transitions[index].condition.checkCondition(state))
            {
                state.currentState = transitions[index].targetState;
                OnExit(state);
                state.currentState.OnEnter(state);
            }
        }
    }

    public void Tick(StateManager state, StateAction[] stateOnList)
    {
        for(int index = 0; index < stateOnList.Length; index++)
        {
            if(stateOnList[index] != null)
            {
                stateOnList[index].Execute(state);
            }
        }
    }

    public Transition AddTransition()
    {
        Transition transitionValue = new Transition();
        transitions.Add(transitionValue);
        return transitionValue;
    }
}