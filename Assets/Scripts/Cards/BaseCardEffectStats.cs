using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class BaseCardEffectStats : ScriptableObject
{
    public abstract int EffectDuration { get; }
    public abstract int EffectOnHealth { get; }
    public abstract string EffectName { get; }
    public abstract CardEffectType BaseCardEffect { get; }
    public abstract List<CardEffectType> ExtraCardEffects { get; }
    public abstract List<CardKeyword> ExtraCardKeywords { get; }
}
