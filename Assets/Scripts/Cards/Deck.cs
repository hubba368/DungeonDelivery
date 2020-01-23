using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    List<CardInfo> _currentDeck;
    [SerializeField]
    List<CardInfo> _currentPlaceholderDeck; // used to store whole deck during combat

    [SerializeField]
    int _maxCardCount = 30;

    public List<CardInfo> CurrentDeck
    {
        get
        {
            return _currentDeck;
        }

        set
        {
            _currentDeck = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        // load ALL cards
        // maybe think about having a 'second' hand to hold combo cards
        // do second loop to get CardAttribs SOs 
        Object[] temp = (Resources.LoadAll("ScriptableObjs/Things/Cards", typeof(CardInfo)));
        Object[] temp2 = (Resources.LoadAll("ScriptableObjs/Things/CardAttribs", typeof(CardAttributes)));

        for (int i = 0; i < temp.Length; ++i)
        {
            var t = (CardInfo)temp[i];

            for (int j = 0; j < temp2.Length; ++j)
            {
                var t2 = (CardAttributes)temp2[j];
            }

            
        }

        for (int i = 0; i < _maxCardCount; ++i)
        {
            var t = (CardInfo)temp[i];
            _currentDeck.Add(t);
        }
    }

    public CardInfo DrawCardInfoFromDeck()
    {
        CardInfo card = null;
        if(_currentDeck.Count > 0)
        {
            card = _currentDeck[0];
            _currentDeck.RemoveAt(0);
        }
        else
        {
            return card;
        }

        return card;
    }
}
