
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

    PlayerHand _playerHandInstance;

    // these will handle sending over card to combathandler
    public void OnPropagateEffect(Card cardData)
    {
        if (PropagateEffectToCharacter != null)
        {
            PropagateEffectToCharacter.Invoke(cardData);
        }
    }
    public Action<Card> PropagateEffectToCharacter;

    public void Init()
    {
        _cardInstance = this.GetComponent<Card>();
        if (_cardInstance)
        {
            _playerHandInstance = Root.GetComponentFromRoot<CombatHandler>().PlayerHand;
            _cardAttributes = _cardInstance.CardInfo.CardAttributes;
            _attachedCardEffect = _cardAttributes.DefaultCardEffect;
            //Debug.Log(_attachedCardEffect);
        }
    }

    public void OnDestroyCard()
    {
        PropagateEffectToCharacter = null;
        Destroy(this.gameObject);
    }
     
    public void OnUseCard()
    {// initialise but do not activate card effect until it is needed.
        if (_attachedCardEffect != null)
        {
            Debug.Log(_cardInstance.CardInfo.CardName);
            var temp = _attachedCardEffect;
            temp.InitialiseCardEffect();
            OnPropagateEffect(_cardInstance);
        }
        
    } 

    // TODO check whether card is combo card might be because just using test effect on current attrib wich is used by all cards
    public void AttachMethodToEffectEvent(Action<Card> method)
    {
        PropagateEffectToCharacter += method;
        Debug.Log(_cardInstance.CardInfo.CardName);
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
