using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Things", menuName = "Things/CardAttributes")]
///<summary>
/// CardAttributes holds the 'in-game' data of a card
/// </summary>
public class CardAttributes : ScriptableObject
{
    [Tooltip("The base damage value of the card.")]
    [SerializeField]
    private int _baseAttack;

    [Tooltip("The base cost of the card.")]
    [SerializeField]
    private int _baseCardCost;

    /* [Tooltip("The target of the card. As bool, will either be player or enemy.")]
     [SerializeField]
     private bool _cardTarget;
     */
    [Tooltip("The base effect that this card can afflict.")]
    [SerializeField]
    private BaseCardEffect _defaultCardEffect;

    [Tooltip("Any extra effects that this card can afflict.")]
    [SerializeField]
    private List<BaseCardEffect> _baseExtraEffects;

    [Tooltip("The value ID that connects the attributes to the cards' main info.")]
    [SerializeField]
    private int _cardInfoID;

    public int BaseAttack
    {
        get
        {
            return _baseAttack;
        }
    }

    public int BaseCardCost
    {
        get
        {
            return _baseCardCost;
        }
    }

    /*public bool CardTarget
    {
        get
        {
            return _cardTarget;
        }
    }*/
    public BaseCardEffect DefaultCardEffect
    {
        get
        {
            return _defaultCardEffect;
        }
    }

    public List<BaseCardEffect> BaseExtraEffects
    {
        get
        {
            return _baseExtraEffects;
        }
    }

    public int CardInfoID
    {
        get
        {
            return _cardInfoID;
        }
    }

    
}
