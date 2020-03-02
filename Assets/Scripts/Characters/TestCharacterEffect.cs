using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CharacterEffects/DebugEffect")]
public class TestCharacterEffect : BaseCharacterEffect
{
    private int _effectDuration;
    private int _effectOnHealth;

    public override int EffectDuration
    {
        get
        {
            return _effectDuration;
        }
    }

    public override int EffectOnHealth
    {
        get
        {
            return _effectOnHealth;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (!EventIsNull())
        {

        }
	}
}
