using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameThings/Cards/ComboCardType")]
public class CombinedCard : CardType
{
    private const int _cardTypeID = 2;

    public override int CardTypeID
    {
        get
        {
            return _cardTypeID;
        }
    }

    public override string GetTypeString()
    {
        return "Combo";
    }

    public override void OnInitCard()
    {
        Debug.Log("this is a combo card");
    }
}
