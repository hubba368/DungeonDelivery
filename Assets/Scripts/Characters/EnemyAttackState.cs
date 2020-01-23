using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
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
        //Debug.Log("Entering ATTACK");
    }

    public void Execute()
    {
        Debug.Log("Executing ATTACK");
        
        //StaticCoroutineRunner.StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        // do attak func and play anim
        // and use card instead of this
        Root.Instance._playerChar.PlayerHealth -= 10.0f;
        yield return new WaitForSeconds(5.0f);

    }

    public void Leave()
    {
        //Debug.Log("Leaving ATTACK");
    }

    public void SetParent(BaseEnemy obj)
    {
        _parent = obj;
    }
}
