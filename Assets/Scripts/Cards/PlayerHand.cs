using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour
{

    Dictionary<int, GameObject> _currentHand = new Dictionary<int, GameObject>();

    [SerializeField]
    GameObject _cardPrototype;
    [SerializeField]
    CardPanel placeholderCardPanel = new CardPanel();

    Deck _currentDeck;
    DiscardPile _currentDiscardPile;
    Card placeholderCard;
    GameObject _currentSelectedCard;
    List<GameObject> _testslot = new List<GameObject>();

    [SerializeField]
    CardPlaceableArea _cardCraftPlacementAreaLeft;
    [SerializeField]
    CardPlaceableArea _cardCraftPlacementAreaRight;
    [SerializeField]
    CardPlaceableArea _cardPlacementAreaMiddle;

    void Awake ()
    {            
        _currentDeck = Root.GetComponentFromRoot<Deck>();
        _currentDiscardPile = Root.GetComponentFromRoot<DiscardPile>();      

        _testslot = GameObject.FindGameObjectsWithTag("CardSlot").ToList();
    }

    public void InitDebugStuff()
    {
        //TODO change this 
        Button button = GameObject.Find("TestEndTurnButton").GetComponent<Button>();
        button.onClick.AddListener(Root.GetComponentFromRoot<CombatHandler>().TestEndTurnAction);
        var temp = GameObject.Find("TestCombineButton").GetComponent<Button>();
        temp.onClick.AddListener(Root.GetComponentFromRoot<CombatHandler>().TestCombineCardAction);

    }

    public void Initialise()
    {
        var tempCard = _cardPrototype;
        _currentHand.Clear();
        placeholderCardPanel = tempCard.GetComponent<CardPanel>();
        placeholderCardPanel.GetComponent<Card>().CreateBlankCard();
        placeholderCardPanel.SetToBlankCard();
        //_currentHand = new Dictionary<int, CardPanel>();

        _currentHand.Add(0, tempCard);
        _currentHand.Add(1, tempCard);
        _currentHand.Add(2, tempCard);
        _currentHand.Add(3, tempCard);
    }

    private GameObject CreateCardFromInfo(CardInfo info, int cardSlot)
    {
        Vector3 scale = _cardPrototype.transform.localScale;
        var t = Instantiate(_cardPrototype, _testslot[cardSlot].transform);
        t.transform.localScale = scale;

        t.GetComponent<Card>().InitialiseCard(info);
        t.GetComponent<CardPanel>().InitCardPanel();
        t.GetComponent<CardLogic>().Init();
        t.GetComponent<CardPanel>().PressedCard += this.OnPressCard;

        return t;
    }

    public void DrawCardFromDeck()
    {
        if(_currentDeck.CurrentDeck.Count <= 0)
        {
            _currentDeck.CurrentDeck = _currentDiscardPile.TakeAllCardsFromDiscardPile();
        }

        AddCraftedCardToHand(Root.GetComponentFromRoot<Deck>().DrawCardInfoFromDeck());

    }

    public void AddCraftedCardToHand(CardInfo newCard)
    {
        int slot = CheckForFreeCardSlot();

        if (slot != -1)
        {
            var temp = CreateCardFromInfo(newCard, slot);
            _currentHand[slot] = temp;
            _currentHand[slot].GetComponent<Card>().isBlank = false;
            temp.GetComponent<Card>().PlayerHandIndex = slot;
            Debug.Log("Card Name: " + temp.GetComponent<Card>().CardInfo.CardName + "\n"
                + "CardType: " + temp.GetComponent<Card>().CardInfo.CardType
                + "Card Cost:" + temp.GetComponent<Card>().CardInfo.CardAttributes.BaseCardCost + "\n");
            // call event here hooking up card logic to combathandler for game logic??
            //temp.GetComponent<CardLogic>().AttachMethodToEffectEvent(PropagateEffectToIntendedTarget);
        }
        else
        {
            Debug.Log("no slot picked");
        }
    }

    public void PropagateEffectToIntendedTarget(BaseCardEffect.EffectData effect)
    {
        Debug.Log("test propagate");
        Root.GetComponentFromRoot<CombatHandler>().HandleCardEffectPropagation(effect);
    }

    public void DiscardAllCardsFromHand()
    {
        Debug.Log("hand count " + _currentHand.Count);
        if (_currentHand.Count <= 0)
            return;

        foreach(var obj in _currentHand)
        {
            if (obj.Value.GetComponent<Card>().isBlank == false)
            {
                Root.GetComponentFromRoot<DiscardPile>().AddToDiscardPile(obj.Value.GetComponent<Card>().CardInfo);
                obj.Value.GetComponent<Card>().CardLogic.OnDestroyCard();
            }

        }
        _currentHand.Clear();
    }

    public void DiscardCardFromHand(Card card)
    {
        _currentHand[card.PlayerHandIndex] = _cardPrototype;
        Root.GetComponentFromRoot<DiscardPile>().AddToDiscardPile(card.CardInfo);
        card.CardLogic.OnDestroyCard();
    }

    private int CheckForFreeCardSlot()
    {
        int result = -1;

        Debug.Log(_currentHand.Count);
        for(int i = 0; i < _currentHand.Count; ++i)
        {
            if (_currentHand[i].GetComponent<CardPanel>().IsBlank == true)
            {
                result = i;
                break;
            }
        }

        return result;
    }

    private void OnPressCard(bool isCardPressed, GameObject cardPanel)
    {
        CardInfo currentCardInfo = null;

        // get cardpanel that has been clicked on from currenthand
        if (_currentHand.ContainsValue(cardPanel))
        {
            var slot = cardPanel.GetComponent<Card>().PlayerHandIndex;
            var temp = _currentHand[slot];
            _currentSelectedCard = temp;
        }
        else
        {
            throw new System.NullReferenceException("Card not found in current player hand.");
        }

        if (isCardPressed)
        {

            currentCardInfo = _currentSelectedCard.GetComponent<Card>().CardInfo;

            if (currentCardInfo.CardType.GetTypeString() != "Combo")
            {
                if(_cardCraftPlacementAreaLeft.CompareCardInArea(currentCardInfo) == false)
                {
                    _cardCraftPlacementAreaLeft.ActivateHighlight(true);
                }
                if(_cardCraftPlacementAreaRight.CompareCardInArea(currentCardInfo) == false)
                {
                    _cardCraftPlacementAreaRight.ActivateHighlight(true);
                }
            }
            else
            {
                if(_cardPlacementAreaMiddle.CompareCardInArea(currentCardInfo) == false)
                {
                    SetCardPlacementArea(_cardPlacementAreaMiddle.gameObject, true);
                }
            }
        }
        else
        {
            // move the cards back to original pos
            currentCardInfo = _currentSelectedCard.GetComponent<Card>().CardInfo;

            if (currentCardInfo.CardType.GetTypeString() != "Combo")
            {   
                if (_cardCraftPlacementAreaLeft.CompareCardInArea(currentCardInfo) == true)
                {
                    SetCardPlacementArea(_cardCraftPlacementAreaLeft.gameObject, false);
                    _cardCraftPlacementAreaLeft.RemoveCardFromArea();

                }
                if (_cardCraftPlacementAreaRight.CompareCardInArea(currentCardInfo) == true)
                {
                    SetCardPlacementArea(_cardCraftPlacementAreaRight.gameObject, false);
                    _cardCraftPlacementAreaRight.RemoveCardFromArea();

                }
            }
            else
            {
                if (_cardPlacementAreaMiddle.CompareCardInArea(currentCardInfo) == true)
                {
                    SetCardPlacementArea(_cardPlacementAreaMiddle.gameObject, false);
                    _cardPlacementAreaMiddle.RemoveCardFromArea();
                }
            }
        }
    }

    private void SetCardPlacementArea(GameObject obj, bool enabled)
    {
        obj.SetActive(enabled);
    }

    public void MoveCardToHighlightArea(Transform area)
    {
        if(_currentSelectedCard!= null)
        {
            _currentSelectedCard.GetComponent<CardPanel>().MoveCardPosition(new Vector2(area.position.x, area.position.y));
            area.GetComponent<CardPlaceableArea>().AttachCardToArea(_currentSelectedCard);
            SetCardPlacementArea(area.gameObject, false);
        }
        
    }

    public void OnUseComboCardOnField()
    {        
        var card = _cardPlacementAreaMiddle.GetAttachedCard();
        
        if (card)
        {
            // TODO make this use events instead to reduce coupling?
            card.GetComponent<CardLogic>().OnUseCard();
        }
    }

    public List<GameObject> GetCardFromCraftingArea()
    {
        var temp = new List<GameObject>();
        temp.Add(_cardCraftPlacementAreaLeft.GetAttachedCard());
        temp.Add(_cardCraftPlacementAreaRight.GetAttachedCard());

        return temp;
    }

    public void EndCardCrafting()
    {
        _cardCraftPlacementAreaLeft.RemoveCardFromArea();
        _cardCraftPlacementAreaRight.RemoveCardFromArea();
        _cardPlacementAreaMiddle.RemoveCardFromArea();
        _cardCraftPlacementAreaLeft.ActivateHighlight(false);
        _cardCraftPlacementAreaRight.ActivateHighlight(false);
        _cardPlacementAreaMiddle.ActivateHighlight(false);
    }
}
