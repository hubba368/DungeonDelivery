using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardType : ScriptableObject
{
    // would use this class to either init type specific things (text etc)
    // or to perform type specific actions
    public abstract void OnInitCard();

    public abstract string GetTypeString();

    public abstract int CardTypeID { get; }
}
