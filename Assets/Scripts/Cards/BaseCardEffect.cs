using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseCardEffect : ScriptableObject
{
    public abstract CardPotentialTarget EffectTarget { get; }
    public abstract BaseCardEffectStats CardEffectDefaults { get; }
    public abstract EffectData CurrentEffectData { get; }
    public abstract CardPotentialState EffectPreferredState { get; }

    public class EffectData
    {
        CardPotentialTarget target;
        BaseCardEffectStats effect;
        string effectName;

        public BaseCardEffectStats Effect
        {
            get
            {
                return effect;
            }
        }

        public string EffectName
        {
            get
            {
                return effectName;
            }
        }

        public CardPotentialTarget Target
        {
            get
            {
                return target;
            }
        }

        public EffectData(CardPotentialTarget t, BaseCardEffectStats e, string eN)
        {
            target = t;
            effect = e;
            effectName = eN;
        }

        public EffectData() { }
    }

    public abstract void InitialiseCardEffect();

    public abstract EffectData InitiateCardEffect();
    public abstract BaseCardEffect OnActivateCardEffect();
}
