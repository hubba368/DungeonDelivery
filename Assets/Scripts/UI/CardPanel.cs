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
    private Button _cardButton;
    
    private bool isBlank = false;
    private bool isCardPressedOn = false;
    [SerializeField]
    private Vector2 _panelOriginalPosition;

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

    public Vector2 PanelOriginalPosition
    {
        get
        {
            return _panelOriginalPosition;
        }
    }

    public event OnPressCardDelegate PressedCard;

    public void InitCardPanel()
    {
        _card = this.GetComponent<Card>();
        _cardLogic = this.GetComponent<CardLogic>();
        _animator = this.GetComponent<Animator>();
        _panelOriginalPosition = this.gameObject.GetComponent<RectTransform>().position;
        _cardButton = this.GetComponent<Button>();
        _cardButton.onClick.AddListener(OnPressCard);

        Root.GetComponentFromRoot<CombatHandler>().PlayerHand.OnResetCardHighlight += Test;
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
            //Debug.Log(this._card.CardInfo.CardName);
            // tell player hand to handle graphics related things that arent directly related to this particular card

            if(!isCardPressedOn)
            {
                PressedCard.Invoke(true, this.gameObject);
                _card.CardHighlightImage.color = new Color(1, 0, 0, 1);
                isCardPressedOn = true;
                //_animator.SetTrigger("Pressed");
 
            }
            else
            {
                OnPressOffCard();
            }

        }
    }

    public void OnPressOffCard()
    {
        if(_cardLogic != null)
        {
            if (isCardPressedOn)
            {
                PressedCard.Invoke(false, this.gameObject);
                isCardPressedOn = false;               
            }
        }
    }
    // TODO rename
    public void Test()
    {
        isCardPressedOn = false;
        //_animator.SetTrigger("NotPressed");
        // move back to 0,0
        this.gameObject.GetComponent<RectTransform>().position = _panelOriginalPosition;
        _card.CardHighlightImage.color = new Color(1, 0, 0, 0);
    }

    public void MoveCardPosition(Vector2 pos)
    {
        this.gameObject.GetComponent<RectTransform>().position = pos;
    }

    private void OnDestroy()
    {// change this at some point so its done via playerhand? 
        PressedCard = null;
        Root.GetComponentFromRoot<CombatHandler>().PlayerHand.OnResetCardHighlight -= Test;
    }
}
