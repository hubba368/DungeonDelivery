using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    public abstract bool EffectTarget { get; }

    public abstract void InitiateCardEffect();
}
