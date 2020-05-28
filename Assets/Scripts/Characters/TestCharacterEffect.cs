using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CharacterEffects/DebugEffect")]
public class TestCharacterEffect : BaseCharacterEffect
{
    [SerializeField]
    private int _effectDuration;
    [SerializeField]
    private int _effectOnHealth;
    [SerializeField]
    private string _effectName;

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

    public override string EffectName
    {
        get
        {
            return _effectName;
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
