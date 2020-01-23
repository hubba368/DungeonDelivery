using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/ActionCardType")]
public class ActionCard : CardType
{
    private const int _cardtypeID = 0;

    public override int CardTypeID
    {
        get
        {
            return _cardtypeID;
        }
    }

    public override void OnInitCard()
    {
        Debug.Log("This is an action card");
    }

    public override string GetTypeString()
    {
        return "Action";
    }
}
