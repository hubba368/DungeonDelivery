using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IState
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
        Debug.Log("entering DEATH");
    }


    public void Execute()
    {
        Debug.Log("executing DEATH");
        MonoBehaviour.Destroy(_parent);
    }

    public void Leave()
    {
        Debug.Log("leaving DEATH");
    }

    public void SetParent(BaseEnemy obj)
    {
        _parent = obj;
    }
}
