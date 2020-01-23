using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IState
{
    BaseEnemy Parent { get;}
    void Enter();
    void Execute();
    void Leave();
    void SetParent(BaseEnemy obj);
}
