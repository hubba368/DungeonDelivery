using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CombatHandler : MonoBehaviour
{

    public BaseEnemy _currentCombatTarget;
    private PlayerController _playerChar;
    private PlayerHand _playerHand;
    private ComboCardCreator _cardCreator;

    [SerializeField]
    private List<GameObject> _selectedCardsForCombination = new List<GameObject>();
    
    private Dictionary<BaseCardEffect, int> _currentActiveCardEffects = new Dictionary<BaseCardEffect, int>();

    private bool _isPlayerTurn = false;

    public UnityAction TestEndTurnAction;
    public UnityAction TestCombineCardAction;

    public UnityAction CardActivateEffectAction;

    public PlayerHand PlayerHand
    {
        get
        {
            return _playerHand;
        }
    }

    private void Awake()
    {
        _playerChar = Root.Instance._playerChar;


        TestEndTurnAction += BeginEnemyTurn;
        TestCombineCardAction += OnCombineCardsButtonPress;
    }

    ~CombatHandler()
    {

    }

    public void StartCombatEncounter(BaseEnemy enemy, PlayerHand hand, ComboCardCreator comboMaker)
    {
        _playerHand = hand;
        _playerHand.AttachMethodToEvent(HandleCardEffectPropagation);
        _playerHand.InitDebugStuff();

        _currentCombatTarget = enemy;
        enemy.InitialiseThingForCombat(100.0f);
        enemy.CombatAI.inCombat = true;
        Debug.Log(enemy.CharacterHealth.ToString());
        var temp = Root.GetComponentFromRoot<UIHandler>().CurrentActivePanel.GetComponentInChildren<EnemyUIPanel>();
        temp.InitPanel(enemy);
        

        _cardCreator = comboMaker;

        StartNewPlayerTurn(hand);
    }

    public void EndCombatEncounter()
    {
        Destroy(this);
    }

    private void StartNewPlayerTurn(PlayerHand hand)
    {
        Debug.Log("starting new player turn");
        hand.Initialise();
        BeginPlayerTurn(hand);
        BeginEnemyStrategise(_currentCombatTarget);
    }

    private void BeginPlayerTurn(PlayerHand hand)
    {
        PlayerDrawCard(hand);
        PlayerDrawCard(hand);
        PlayerDrawCard(hand);
        PlayerDrawCard(hand);
        PlayerGainMana(5);
    }

    public void BeginEnemyTurn()
    {
        Debug.Log("starting new enemy turn");
        PlayerEndTurn();
        _currentCombatTarget.CombatAI.InitiateMove();
        // TODO: each enemy type has its own moveset 
        StartNewPlayerTurn(_playerHand);
    }

    private void BeginEnemyStrategise(BaseEnemy enemy)
    {
        Debug.Log("enemy is thinking about hand");
        enemy.CombatAI.InitiateThink();
    }

    private void PlayerEndTurn()
    {// TODO: add card effect checking

        HandleAnyCardEffectsInPlay(CardPotentialState.OnEndTurn);

        _isPlayerTurn = false;
        PlayerDiscardHand();
        QuitCardCraftingPhase();
        //BeginEnemyTurn();

    }

    private void PlayerDrawCard(PlayerHand hand)
    {
        hand.DrawCardFromDeck();
        HandleAnyCardEffectsInPlay(CardPotentialState.OnDraw);
    }

    private void PlayerDiscardCard(PlayerHand hand, Card card)
    {
        hand.DiscardCardFromHand(card);
    }

    private void PlayerDiscardHand()
    {
        _playerHand.DiscardAllCardsFromHand();
    }

    private void PlayerGainMana(int amount)
    {
        _playerChar.IncrementPlayerMana(amount);
    }

    public void AddCardToComboPool(GameObject card)
    {
        _selectedCardsForCombination.Add(card);
    }

    public void QuitCardCraftingPhase()
    {
        _playerHand.EndCardCrafting();
        if (_selectedCardsForCombination.Count > 0)
        {
            _selectedCardsForCombination.Clear();
        }
    }

    public void OnCombineCardsButtonPress()
    {
        CombineCards();
    }

    private void CombineCards()
    {

        if (_cardCreator != null)
        {
            _selectedCardsForCombination = _playerHand.GetCardFromCraftingArea();

            var result = _cardCreator.CombineCards(_selectedCardsForCombination[0].GetComponent<Card>(), _selectedCardsForCombination[1].GetComponent<Card>());

            if (result != null)
            {
                _playerHand.DiscardCardFromHand(_selectedCardsForCombination[0].GetComponent<Card>());
                _playerHand.DiscardCardFromHand(_selectedCardsForCombination[1].GetComponent<Card>());
                Debug.Log(result.CardName);
                _playerHand.AddCraftedCardToHand(result);
                QuitCardCraftingPhase();
            }

        }
    }

    public void HandleCardEffectPropagation(Card card)
    {
        var currentCardEffect = card.CardInfo.CardAttributes.DefaultCardEffect;
        /*Debug.Log("test propagate");
        Debug.Log("name = " + currentCardEffect.CardEffectDefaults.EffectName + 
            " duration = " + currentCardEffect.CardEffectDefaults.EffectDuration + 
            " health = " + currentCardEffect.CardEffectDefaults.EffectOnHealth);*/
        
        OnPlayerUseCard(card);
    }

    // will probs use this as literal initial card usage, i.e player uses card and it immediately does its intended func.
    // TODO: create 'cardeventhandler' class that will handle how to use the card effects rather than letting combat handler do it.
    // all we would do is call eventhandler.OnUseCard(cardAttrib attrib) then return the result and attrib with edited values e.g. duration decrease.
    // for end of turn etc, would use eventhandler.CheckAllEffects() which would return lsit of edited attribs and whatever occurs
    private void OnPlayerUseCard(Card card)
    {
        var cardAttribs = card.CardInfo.CardAttributes;
        var currentCardEffect = card.CardInfo.CardAttributes.DefaultCardEffect;
        // TODO; change damage to a damage event
        if (cardAttribs.BaseAttack > 0)
        {
            if (ChangeTargetHealth(GetTarget(currentCardEffect.EffectTarget), card.CardInfo.CardAttributes.BaseAttack) == 0)
            {
                Debug.Log("target health is zero.");
            }
        }

        cardAttribs.DefaultCardEffect.OnActivateCardEffect();
        _currentActiveCardEffects.Add(currentCardEffect, cardAttribs.DefaultCardEffect.CardEffectDefaults.EffectDuration);

    }

    private BaseCharacter GetTarget(CardPotentialTarget target)
    {
        BaseCharacter result = null;
        switch (target)
        {
            case CardPotentialTarget.Debug:
                break;
            case CardPotentialTarget.Enemy:
                result = _currentCombatTarget;
                break;
            case CardPotentialTarget.Player:
                //return _playerChar;// add basic ITargetable interface or something
                break;
        }
        return result;
    }

    private int ChangeTargetHealth(BaseCharacter target, int amount)
    {
        // check if target dies here to skip extra stuff
        var enemy = target as BaseEnemy;
        enemy.CharacterHealth -= amount;
        var temp = (EnemyUIPanel)GameObject.Find("EnemyUIPanel").GetComponent<EnemyUIPanel>();
        temp.UpdateEnemyHealth((int)enemy.CharacterHealth);

        if (enemy.CharacterHealth <= 0)
        {
            return 0;
        }
        return (int)enemy.CharacterHealth;
    }

    private void HandleAnyCardEffectsInPlay(CardPotentialState curState)
    {
        HandleCardEffectsWorker(curState);
    }

    private void HandleCardEffectsWorker(CardPotentialState curState)
    {
        if (_currentActiveCardEffects.Count > 0)
        {// get effects by their 'intended' usage state
            // would eventually move entire thing to event system so i could hook it up to 
            // anims/fx etc and so it can be 'slowed' down
            var tempEffects = _currentActiveCardEffects.Where(x => x.Key.EffectPreferredState == curState).ToList();

            for(int i = 0; i < tempEffects.Count; i++)
            {
                var e = tempEffects[i].Key;
                e.OnActivateCardEffect();
                // would move code below to the effect itself, and it would simply invoke a method here instead
                if(e.CardEffectDefaults.BaseCardEffect == CardEffectType.Debug)
                {
                    ChangeTargetHealth(GetTarget(e.EffectTarget), e.CardEffectDefaults.EffectOnHealth);
                }
                _currentActiveCardEffects[e]--;

                if (_currentActiveCardEffects[e] <= 0)
                {
                    _currentActiveCardEffects.Remove(e);
                }
            }
            
            /*var tempList = new List<Card>();
            for (int i = 0; i < _currentActiveCardEffects.Count; i++)
            {
                var effect = _currentActiveCardEffects.ElementAt(i);
                var newEffectChange = effect.Key;
                newEffectChange.OnActivateCardEffect();
                _currentActiveCardEffects[effect.Key]--;

                if (effect.Value <= 0)
                {
                    _currentActiveCardEffects.Remove(effect.Key);
                }
            }*/
            //
        }
    }
}

