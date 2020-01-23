
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// acts as an instance class for cardattributes
public class CardLogic : MonoBehaviour
{
    // this class will handle a cards in game behaviour
    Card _cardInstance;

    public void OnDestroyCard()
    {
        Destroy(this.gameObject);
    }
     
    public void OnPressCard()
    {

    } 
    //TODO 
    //funcs we probably need:
    // Combination of action and item card func
    // will need to think about how to combine cards when added to the combo area
    // some kind of card combinator class that takes the two cards when a button press
    // then could either do lookup in a table or straight up randomised cards

    // 'use' card func
    // for action and item cards, this would simply add them to the combination area on the UI
    // for combined cards, would need to probs use an override 'doCardAction' func


    // stat related func e.g. changing of stats when in hand or when combined
    // would need to remember to cap low vals to zero

}
