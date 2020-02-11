using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCardCreator : MonoBehaviour
{
    //use this class to combine action and item cards together

    public CardInfo CombineCards(Card lhc, Card rhc)
    {
        if (!CheckCraftingValidity(lhc, rhc))
        {
            return null; 
        }

        CardInfo result;

        int resultantKey = lhc.CardInfo.CardCraftingID + rhc.CardInfo.CardCraftingID;

        result = Root.GetComponentFromRoot<CardCraftingDatatable>().GetRecipeByKey(resultantKey);

        return result;
    }

    private bool CheckCraftingValidity(Card lhc, Card rhc)
    {
        if (lhc.CardInfo.CardType.CardTypeID == rhc.CardInfo.CardType.CardTypeID)
        {
            Debug.Log("Attempted to combine invalid cards");
            Debug.Log(lhc.CardInfo.CardType.CardTypeID + " " + rhc.CardInfo.CardType.CardTypeID);
            return false;
        }
        else if(lhc.CardInfo.CardType.GetTypeString() == "Combo" || rhc.CardInfo.CardType.GetTypeString() == "Combo")
        {
            Debug.Log("Can't combine an already combined card");
            return false;
        }
        else
        {
            return true;
        }
    }


}
