using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCardCreator : MonoBehaviour
{
    //use this class to combine action and item cards together

    public void CombineCards(Card lhc, Card rhc)
    {
        if(lhc.CardInfo.CardType.CardTypeID == rhc.CardInfo.CardType.CardTypeID)
        {
            Debug.Log("Attempted to combine invalid cards");
            return;
        }

        Debug.Log(lhc.CardInfo.CardName);
        Debug.Log(rhc.CardInfo.CardName);
    }


}
