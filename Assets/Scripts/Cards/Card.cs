using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField]
    CardInfo _cardInfo;
    //[SerializeField]
    CardAttributes _cardAttributes;
    [SerializeField]
    CardType _cardType;
    [SerializeField]
    Image _cardIcon;
    [SerializeField]
    Text _cardName;
    [SerializeField]
    Text _cardDescription;
    [SerializeField]
    Text _cardTypeText;
    [SerializeField]
    Text _cardCostValue;
    [SerializeField]
    Image _cardHighlightImage;
    [SerializeField]
    int _playerHandIndex; // use this for player hand related funcs

    public bool isBlank = false;

    public CardInfo CardInfo
    {
        get
        {
            return _cardInfo;
        }
    }

    public Image CardHighlightImage
    {
        get
        {
            return _cardHighlightImage;
        }
    }

    public int PlayerHandIndex
    {
        get
        {
            return _playerHandIndex;
        }

        set
        {
            _playerHandIndex = value;
        }
    }

    public void InitialiseCard(CardInfo info)
    {
        _cardInfo = info;
        _cardIcon.sprite = _cardInfo.CardIcon;
        _cardName.text = _cardInfo.CardName;
        _cardDescription.text = _cardInfo.CardDescription;
        _cardAttributes = _cardInfo.CardAttributes;
        _cardType = _cardInfo.CardType;
        _cardTypeText.text = _cardType.GetTypeString();
        _cardCostValue.text = _cardAttributes.BaseCardCost.ToString();
    }

    public void CreateBlankCard()
    {
        isBlank = true;
    }

    
        // big TODO:
        // need to think about how to handle card attributes, like attack, debuffs, buffs etc.
        // dont use enums as that would become too much of a hassle to maintain
        // maybe use a struct? but then have problem of some cards having unused values
        // attributes:
        /*
            if going by slay the spire type, it is 'attack' cards and spell cards
            attack value            - int            
            card mana cost          - int
            'intended target'       - probs bool
            debuff/buff on target   - e.g. poison, burn, card mulligan/discard
                - would also need duration if applicable
                - this would probs be a either an enum/static class (statusType)

            health increase/decrease value - int

            //potential additions
            'curse' cards, e.g. cards that you have to get out of your hand or suffer consequences
        */
    }
