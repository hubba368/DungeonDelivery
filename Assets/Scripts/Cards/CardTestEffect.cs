using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/CardEffects/DebugEffect")]
public class CardTestEffect : BaseCardEffect
{
    [SerializeField]
    CardPotentialTarget _effectTarget;
    [SerializeField]
    BaseCardEffectStats _cardEffectDefaults;
    [SerializeField]
    string EffectIDString = "Debug";
    [SerializeField]
    EffectData _currentEffectData;
    [SerializeField]
    CardPotentialState _cardPotentialState;


    public override CardPotentialTarget EffectTarget
    {
        get
        {
            return _effectTarget;
        }
    }

    public override BaseCardEffectStats CardEffectDefaults
    {
        get
        {
            return _cardEffectDefaults;
        }
    }

    public override EffectData CurrentEffectData
    {
        get
        {
            return _currentEffectData;
        }
    }

    public override CardPotentialState EffectPreferredState
    {
        get
        {
            return _cardPotentialState;
        }
    }

    public override EffectData InitiateCardEffect()
    {
        EffectData temp = new EffectData(_effectTarget, _cardEffectDefaults, _cardEffectDefaults.EffectName);
        
        return temp;
    }

    public override void InitialiseCardEffect()
    {
        //Debug.Log("creating card effect of type: " + effectType.ToString());
        _currentEffectData = InitiateCardEffect(); 
    }

    public override BaseCardEffect OnActivateCardEffect()
    {
        Debug.Log("Initiating Card Effect of Type: " + this.EffectIDString);
        Debug.Log("Current Effect Data: ");
        Debug.Log("Duration: " + _currentEffectData.Effect.EffectDuration);
        Debug.Log("Health Effect: " + _currentEffectData.Effect.EffectOnHealth);
        Debug.Log("Name: " + _currentEffectData.Effect.EffectName);
        Debug.Log("Current Effects Attached Abilities: ");
        foreach(var x in _cardEffectDefaults.ExtraCardEffects)
        {
            Debug.Log(x.ToString());
        }
        Debug.Log("Current Effects Attached Keywords: ");
        foreach (var x in _cardEffectDefaults.ExtraCardKeywords)
        {
            Debug.Log(x.ToString());
        }
      

        return this;
    }
}
