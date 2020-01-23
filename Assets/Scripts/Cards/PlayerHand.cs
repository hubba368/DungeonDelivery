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

    [SerializeField]
    GameObject _cardCraftPlacementAreaLeft;
    [SerializeField]
    GameObject _cardCraftPlacementAreaRight;
    [SerializeField]
    GameObject _cardPlacementAreaMiddle;

    void Awake ()
    {      
        //TODO change this 
        Button button = GameObject.Find("TestEndTurnButton").GetComponent<Button>();
        button.onClick.AddListener(Root.GetComponentFromRoot<EncounterHandler>().TestEndTurnAction);
        var temp = GameObject.Find("TestCombineButton").GetComponent<Button>();
        temp.onClick.AddListener(Root.GetComponentFromRoot<EncounterHandler>().TestCombineCardAction);

        _currentDeck = Root.GetComponentFromRoot<Deck>();
        _currentDiscardPile = Root.GetComponentFromRoot<DiscardPile>();
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
        var t = Instantiate(_cardPrototype, this.transform.GetChild(cardSlot));
        t.transform.localScale = scale;

        t.GetComponent<Card>().InitialiseCard(info);
        t.GetComponent<CardPanel>().InitCardPanel();

        t.GetComponent<CardPanel>().PressedCard += this.OnPressCard;

        return t;
    }

    public void DrawCardFromDeck()
    {
        if(_currentDeck.CurrentDeck.Count <= 0)
        {
            _currentDeck.CurrentDeck = _currentDiscardPile.TakeAllCardsFromDiscardPile();
        }

        int slot = CheckForFreeCardSlot();

        if(slot != -1)
        {
            var temp = CreateCardFromInfo(Root.GetComponentFromRoot<Deck>().DrawCardInfoFromDeck(), slot);
            _currentHand[slot] = temp;

            Debug.Log("Card Name: " + temp.GetComponent<Card>().CardInfo.CardName + "\n"
                + "CardType: " + temp.GetComponent<Card>().CardInfo.CardType
                + "Card Cost:" + temp.GetComponent<Card>().CardInfo.CardAttributes.BaseCardCost + "\n");
        }
        else
        {
            Debug.Log("no slot picked");
        }
    }

    public void DiscardAllCardsFromHand()
    {
        Debug.Log("hand count " + _currentHand.Count);
        if (_currentHand.Count <= 0)
            return;

        foreach(var obj in _currentHand)
        {
            
            //todo update this to use cardlogic class to let cards destroy themselves
            //Debug.Log("destroying card: " + obj.Value.GetComponent<Card>().CardInfo.CardName);
            DiscardCardFromHand(obj.Value.GetComponent<Card>());
            obj.Value.gameObject.GetComponent<CardLogic>().OnDestroyCard();
        }
        _currentHand.Clear();
    }

    public void DiscardCardFromHand(Card card)
    {
        Root.GetComponentFromRoot<DiscardPile>().AddToDiscardPile(card.CardInfo);
        
    }

    private int CheckForFreeCardSlot()
    {
        int result = -1;

        if(_currentHand == null)
        {
            Debug.Log("null");
        }

        for(int i = 0; i < _currentHand.Count; ++i)
        {
            if (_currentHand[i].GetComponent<CardPanel>().IsBlank == true)
            {
                result = i;
                //_currentHand.Remove(i);
                break;
            }
        }

        return result;
    }

    private void OnPressCard(bool isCardPressed, GameObject cardPanel)
    {
        if (isCardPressed)
        {
            // get cardpanel that has been clicked on from currenthand
            if (_currentHand.ContainsValue(cardPanel))
            {
                var temp = _currentHand.Values.Where(item => item == cardPanel);
                _currentSelectedCard = temp.ElementAt(0); 
            }


            if(cardPanel.GetComponent<CardPanel>().Card.CardInfo.CardType.GetTypeString() != "Combo")
            {
                SetCardPlacementArea(_cardCraftPlacementAreaLeft, true, new Color(1, 0, 0, 1));
                SetCardPlacementArea(_cardCraftPlacementAreaRight, true, new Color(1, 0, 0, 1));
            }
            else
            {
                SetCardPlacementArea(_cardPlacementAreaMiddle, true, new Color(1, 0, 0, 1));
            }
        }
        else
        {
            if (cardPanel.GetComponent<CardPanel>().Card.CardInfo.CardType.GetTypeString() != "Combo")
            {
                SetCardPlacementArea(_cardCraftPlacementAreaLeft, false, new Color(1, 0, 0, 0));
                SetCardPlacementArea(_cardCraftPlacementAreaRight, false, new Color(1, 0, 0, 0));
            }
            else
            {
                SetCardPlacementArea(_cardPlacementAreaMiddle, false, new Color(1, 0, 0, 0));
            }
        }
    }

    private void SetCardPlacementArea(GameObject obj, bool enabled, Color color)
    {
        obj.SetActive(enabled);
        obj.GetComponent<Image>().color = color;
    }

    public void MoveCardToNewArea()
    {
        if(_currentSelectedCard!= null)
        {
            //_currentSelectedCard.GetComponent<RectTransform>().anchoredPosition = EventSystem.current.currentSelectedGameObject.transform.localPosition;

            Root.GetComponentFromRoot<EncounterHandler>().AddCardToComboPool(_currentSelectedCard);
        }
        
    }

    public void MoveCardToOriginalPos()
    {

    }
}
