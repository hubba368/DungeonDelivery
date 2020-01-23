using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    BaseEnemy _parent;

    BaseEnemy IState.Parent
    {
        get
        {
            return _parent;
        }
    }

    public void Enter()
    {
        //Debug.Log("entering IDLE");
    }

    public void Execute()
    {
        Debug.Log("enemy checking hand for potential moves...");
        //_parent.CombatAI.DrawCardFromDeck();

        Debug.Log("enemy has found move...");
        _parent.CombatAI.hasFoundPotentialMove = true;

        //StaticCoroutineRunner.StartCoroutine(StartIdle());
    }

    IEnumerator StartIdle()
    {
       // _parent.CombatAI.DrawCardFromDeck();
       // _parent.CombatAI.GainMana();
        yield return new WaitForSeconds(1.0f);
    }

    public void Leave()
    {
        //Debug.Log("Leaving IDLE");
    }

    public void SetParent(BaseEnemy obj)
    {
        _parent = obj;
    }
}
