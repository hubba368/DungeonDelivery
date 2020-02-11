using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CardEffects/DebugEffect")]
public class CardTestEffect : BaseCardEffect
{

    bool _effectTarget;

    public override bool EffectTarget
    {
        get
        {
            return _effectTarget;
        }
    }

    public override void InitiateCardEffect()
    {
        Debug.Log("starting test card effect");
        _effectTarget = false;

        Debug.Log("running effect tests..");
        Debug.Log("running player/enemy ");

    }
}
