using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public delegate void OnPressCardDelegate(bool isPressed, GameObject card);

public class CardPanel : MonoBehaviour
{

    private Card _card;
    private CardLogic _cardLogic;
    private Animator _animator;
    
    private bool isBlank = false;
    private bool isCardPressedOn = false;

    public Card Card
    {
        get
        {
            return _card;
        }
    }

    public bool IsBlank
    {
        get
        {
            return isBlank;
        }
    }

    public CardLogic CardLogic
    {
        get
        {
            return _cardLogic;
        }
    }

    public event OnPressCardDelegate PressedCard;

    public void InitCardPanel()
    {
        _card = this.GetComponent<Card>();
        _cardLogic = this.GetComponent<CardLogic>();
        _animator = this.GetComponent<Animator>();
    }

    public void SetToBlankCard()
    {
        isBlank = true;
        this.enabled = false;
    }

    public void OnPressCard()
    {
        if (_cardLogic != null)
        {
            // tell player hand to handle graphics related things that arent directly related to this particular card
            if (isCardPressedOn)
            {
                PressedCard.Invoke(false, this.gameObject);
                //_animator.SetTrigger("NotPressed");
                
                isCardPressedOn = false;
                _card.CardHighlightImage.color = new Color(1, 0, 0, 0);
                // move back to 0,0
            }
            else
            {
                PressedCard.Invoke(true, this.gameObject);
                _card.CardHighlightImage.color = new Color(1, 0, 0, 1);
                _cardLogic.OnPressCard();
                isCardPressedOn = true;
                //_animator.SetTrigger("Pressed");
 
            }

        }
    }
}
