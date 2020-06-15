using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CharacterEffects/DebugEffect")]
public class TestCharacterEffect : BaseCardEffectStats
{
    [SerializeField]
    private int _effectDuration;
    [SerializeField]
    private int _effectOnHealth;
    [SerializeField]
    private string _effectName;
    [SerializeField]
    CardEffectType _baseCardEffect;
    [SerializeField]
    List<CardEffectType> _cardEffects;
    [SerializeField]
    List<CardKeyword> _cardKeywords;


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

    public override CardEffectType BaseCardEffect
    {
        get
        {
            return _baseCardEffect;
        }
    }
    public override List<CardEffectType> ExtraCardEffects
    {
        get
        {
            return _cardEffects;
        }
    }

    public override List<CardKeyword> ExtraCardKeywords
    {
        get
        {
            return _cardKeywords;
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
}
