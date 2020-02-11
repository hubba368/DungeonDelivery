
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// acts as an instance class for cardattributes
public class CardLogic : MonoBehaviour
{
    // this class will handle a cards in game behaviour
    Card _cardInstance;
    CardAttributes _cardAttributes;
    BaseCardEffect _attachedCardEffect;

    private void Start()
    {
        _cardInstance = this.GetComponent<Card>();
        if (_cardInstance)
        {
            _cardAttributes = _cardInstance.CardInfo.CardAttributes;
            _attachedCardEffect = _cardAttributes.BaseEffect;
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
            _attachedCardEffect.InitiateCardEffect();
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
