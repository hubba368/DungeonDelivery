using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/ItemCardType")]
public class ItemCard : CardType
{
    private const int _cardTypeID = 1;

    public override int CardTypeID
    {
        get
        {
            return _cardTypeID;
        }
    }

    public override string GetTypeString()
    {
        return "Item";
    }

    public override void OnInitCard()
    {
        Debug.Log("this is an item card");
    }
}
