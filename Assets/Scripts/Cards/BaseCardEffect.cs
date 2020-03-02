using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    public abstract bool EffectTarget { get; }
    public abstract BaseCharacterEffect CharacterEffect { get; }

    public struct EffectData
    {
        bool target;
        BaseCharacterEffect effect;

        public BaseCharacterEffect Effect
        {
            get
            {
                return effect;
            }
        }

        public EffectData(bool t, BaseCharacterEffect e)
        {
            target = t;
            effect = e;
        }
    }

    public abstract EffectData InitiateCardEffect();
}
