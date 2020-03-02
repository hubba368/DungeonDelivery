using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseCharacterEffect : ScriptableObject
{
    public abstract int EffectDuration { get; }
    public abstract int EffectOnHealth { get; }

    public event Func<BaseCharacterEffect> PropagateEffectToCharacter;
    protected bool EventIsNull()
    {
        return PropagateEffectToCharacter == null;
    }
}
