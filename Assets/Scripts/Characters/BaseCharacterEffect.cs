using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseCharacterEffect : ScriptableObject
{
    public abstract int EffectDuration { get; }
    public abstract int EffectOnHealth { get; }
    public abstract string EffectName { get; }

    public delegate BaseCharacterEffect PropagateEffectToCharacter();
    public event PropagateEffectToCharacter OnPropagate;
    protected bool EventIsNull()
    {
        return OnPropagate == null;
    }
}
