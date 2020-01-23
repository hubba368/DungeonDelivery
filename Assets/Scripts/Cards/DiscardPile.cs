using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    Stack<CardInfo> _currentDiscardPile;

    public Stack<CardInfo> CurrentDiscardPile
    {
        get
        {
            return _currentDiscardPile;
        }

        set
        {
            _currentDiscardPile = value;
        }
    }

    private void Awake()
    {
        _currentDiscardPile = new Stack<CardInfo>();
    }

    public void AddToDiscardPile(CardInfo card)
    {
        _currentDiscardPile.Push(card);
    }

    public CardInfo TakeFromDiscardPile()
    {
        return _currentDiscardPile.Pop();
    }

    public List<CardInfo> TakeAllCardsFromDiscardPile()
    {
        List<CardInfo> result = new List<CardInfo>();
        int count = _currentDiscardPile.Count;

        for(int i = 0; i < count; ++i)
        {
            result.Add(_currentDiscardPile.Pop());
        }

        return result;
    }

}
