using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFSM : MonoBehaviour
{
    // todo update this to use deterministic transitions?
    private List<IState> _allStates;

    protected IState CurrentState { get; set; }
    protected IState PreviousState { get; set; }

    protected void ChangeState(IState newState)
    {
        PreviousState = CurrentState;

        if(CurrentState != null)
        {
            CurrentState.Leave();
        }
        CurrentState = newState;
        
        CurrentState.Enter();
    }

    protected void ReturnToPreviousState()
    {
        if(PreviousState != null)
        {
            ChangeState(PreviousState);
        }
    }

}
