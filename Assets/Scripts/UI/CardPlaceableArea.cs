using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlaceableArea : MonoBehaviour, ICardHighlightArea
{

    protected PlayerHand _playerHand;
    [SerializeField]
    protected GameObject _attachedCard;
    [SerializeField]
    string test;
    [SerializeField]
    protected Button _areaButton;

    public virtual bool CompareCardInArea(CardInfo card)
    {
        if(_attachedCard == null)
        {
            return false;
        }
        else if(card.CardName == _attachedCard.GetComponent<Card>().CardInfo.CardName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void OnClickArea() { }

    public virtual void AttachCardToArea(GameObject card)
    {
        _attachedCard = card;
        test = _attachedCard.GetComponent<Card>().CardInfo.CardName;
    }

    public virtual void RemoveCardFromArea()
    {
        _attachedCard = null;
    }

    public GameObject GetAttachedCard()
    {
        if(_attachedCard != null)
        {
            return _attachedCard;
        }
        else
        {
            return null;
        }
    }

    public void ActivateHighlight(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }
}
