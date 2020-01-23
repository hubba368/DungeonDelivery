using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Things", menuName = "Things/CardInfo")]
public class CardInfo : ScriptableObject
{
    [Tooltip("The attributes that this card can have.")]
    [SerializeField]
    private CardAttributes _cardAttributes;

    [Tooltip("The type that this card inherits from.")]
    [SerializeField]
    private CardType _cardType;

    [Tooltip("In-game sprite of the cards image")]
    [SerializeField]
    private Sprite _cardIcon;

    [Tooltip("The name of the card")]
    [SerializeField]
    private string _cardName;

    [Tooltip("The description of what the card does")]
    [SerializeField]
    private string _cardDescription;

    [Tooltip("The ID link to this cards' attributes")]
    [SerializeField]
    private int _cardAttribID;

    public Sprite CardIcon
    {
        get
        {
            return _cardIcon;
        }
    }

    public string CardName
    {
        get
        {
            return _cardName;
        }
    }

    public string CardDescription
    {
        get
        {
            return _cardDescription;
        }
    }

    public CardAttributes CardAttributes
    {
        get
        {
            return _cardAttributes;
        }
    }

    public int CardAttribID
    {
        get
        {
            return _cardAttribID;
        }
    }

    public CardType CardType
    {
        get
        {
            return _cardType;
        }
    }
}
