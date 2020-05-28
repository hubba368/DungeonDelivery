using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CardEffects/DebugEffect")]
public class CardTestEffect : BaseCardEffect
{

    bool _effectTarget;
    [SerializeField]
    BaseCharacterEffect _characterEffect;

    public override bool EffectTarget
    {
        get
        {
            return _effectTarget;
        }
    }

    public override BaseCharacterEffect CharacterEffect
    {
        get
        {
            return _characterEffect;
        }
    }

    // for this, would do effects that happen when a card is initially played?
    public override EffectData InitiateCardEffectOnSelf()
    {
        Debug.Log("starting test card effect");
        _effectTarget = false;
        EffectData temp = new EffectData(_effectTarget, _characterEffect);

        return temp;
    }
}
