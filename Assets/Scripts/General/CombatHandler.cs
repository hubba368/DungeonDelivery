using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatHandler : MonoBehaviour
{

    private BaseEnemy _currentCombatTarget;
    private PlayerController _playerChar;
    public PlayerHand _playerHand;
    private ComboCardCreator _cardCreator;

    [SerializeField]
    private List<GameObject> _selectedCardsForCombination = new List<GameObject>();

    private bool _isPlayerTurn = false;

    public UnityAction TestEndTurnAction;
    public UnityAction TestCombineCardAction;

    public UnityAction CardActivateEffectAction;

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
        hand.InitDebugStuff();

        _currentCombatTarget = enemy;
        enemy.InitialiseThingForCombat(100.0f);
        enemy.CombatAI.inCombat = true;
        var temp = Root.GetComponentFromRoot<UIHandler>().CurrentActivePanel.GetComponent<EnemyUIPanel>();
        //temp.UpdateEnemyHealth((int)enemy.CharacterHealth);
        

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
    {
        _isPlayerTurn = false;
        PlayerDiscardHand();
        QuitCardCraftingPhase();
        //BeginEnemyTurn();

    }

    private void PlayerDrawCard(PlayerHand hand)
    {
        hand.DrawCardFromDeck();
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

    public void HandleCardEffectPropagation(BaseCardEffect.EffectData effect)
    {
        // handle updating of card effects here?
        // send event invocations out that will attach to baseenemy/playerchar
        // combat handler WILL handle attached effects after every turn / start of a player turn.
        Debug.Log("Combat Handler recieved new Card Effect." +
            " Recieved: " + effect.Effect.EffectName + "\n" +
            "Effect Target = " + effect.Target + "\n" +
            "Effect on health = " + effect.Effect.EffectOnHealth + "\n" +
            "Effect Duration Num of Turns = " + effect.Effect.EffectDuration);

        switch (effect.Target)
        { // true = player false = enemy
            // TODO should probs change this maybe
            case true:
                _playerChar.AttachedCardEffects.Add(effect.Effect);
                break;
            case false:
                _currentCombatTarget.AttachedCardEffects.Add(effect.Effect);
                break;
        }
    }

    private void UpdateCardEffectsAttachedToChars()
    {

    }
}
