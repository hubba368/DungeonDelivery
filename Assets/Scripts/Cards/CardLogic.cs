
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// acts as an instance class for cardattributes
public class CardLogic : MonoBehaviour
{
    // this class will handle a cards in game behaviour
    [SerializeField]
    Card _cardInstance;
    CardAttributes _cardAttributes;
    BaseCardEffect _attachedCardEffect;

    public void Init()
    {
        _cardInstance = this.GetComponent<Card>();
        if (_cardInstance)
        {
            _cardAttributes = _cardInstance.CardInfo.CardAttributes;
            _attachedCardEffect = _cardAttributes.BaseEffect;
            Debug.Log(_attachedCardEffect);
        }
    }

    public void OnDestroyCard()
    {
        Destroy(this.gameObject);
    }
     
    public void OnUseCard()
    {
        if (_attachedCardEffect)
        {           
            var temp = _attachedCardEffect.InitiateCardEffectOnSelf();
            Root.GetComponentFromRoot<CombatHandler>()._playerHand.PropagateEffectToIntendedTarget(temp);

            //TODO: figure out way to have no coupling between card logic and effect targets
            //temp.Effect.OnPropagate.Invoke();
        }

    } 

    public void AttachMethodToEffectEvent(Action method)
    {
        if (_attachedCardEffect)
        {
            //_attachedCardEffect.CharacterEffect.OnPropagate += method;
        }
        else
        {
            Debug.Log(_cardInstance.CardInfo.CardName);
        }
        
    }

    public void OnCardStatsChange()
    {
        if(_cardAttributes == null)
        {
            throw new System.NullReferenceException("card has no attributes that can be changed.");

        }


    }
    //TODO 
    //funcs we probably need:

    // 'use' card func
    // for action and item cards, this would simply add them to the combination area on the UI
    // for combined cards, would need to probs use an override 'doCardAction' func


    // stat related func e.g. changing of stats when in hand or when combined
    // would need to remember to cap low vals to zero

}
