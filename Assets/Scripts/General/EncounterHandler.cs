using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EncounterHandler : MonoBehaviour
{
    
    //TODO change this to retrival of random prefab/ based on 'area' maybe 
    [SerializeField]
    GameObject EnemyTest;
    [SerializeField]
    GameObject ItemTest;
    [SerializeField]
    PlayerController _playerChar;
    [SerializeField]
    GameObject _comboCardCreatorPrefab;
    [SerializeField]
    GameObject _currentEncounterThing;
    [SerializeField]
    BaseEnemy _currentEnemyInCombat;
    [SerializeField]
    List<GameObject> _selectedCardsForCombination = new List<GameObject>();

    ComboCardCreator _cardCreator;
    PlayerHand _playerCombatHand;

    bool _playerInCombat = false;
    bool _isPlayerTurn = false;

    public UnityAction TestEndTurnAction;
    public UnityAction TestCombineCardAction;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        TestEndTurnAction += BeginEnemyTurn;
        TestCombineCardAction += OnCombineCardsButtonPress;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerChar = Root.Instance._playerChar;
        _cardCreator = Instantiate(_comboCardCreatorPrefab).GetComponent<ComboCardCreator>();
    }

    public void StartEncounter(EncounterType type, Vector3 spawnPos)
    {
        switch (type)
        {// TODO: finish encounter spawning an shit
            // enemy with random chosen sprite, 'personality' (for dialogue)
            // random item
            // goal
            case EncounterType.Item:
                SpawnItemEncounter(ItemTest, spawnPos);
                break;
            case EncounterType.Character:
                //if (Random.Range(0, 2) == 0)
                SpawnCharacterEncounter(EnemyTest, spawnPos, CharacterPersonality.NONHOSTILE);
                break;
            case EncounterType.Goal:
                break;
        }

        Root.GetComponentFromRoot<UIHandler>().ShowUIPanelGeneric(UIPanelType.PlayerInteract);
    }

    private void SpawnCharacterEncounter(GameObject obj, Vector3 spawnPos, CharacterPersonality type)
    {// maybe create enemy class here then add it as a component to the gameobject?
        _currentEncounterThing = Instantiate(obj, spawnPos, Quaternion.identity);
        obj.GetComponent<BaseEnemy>().InitialiseThingPersonality(type);
    }

    private void SpawnItemEncounter(GameObject obj, Vector3 spawnPos)
    {
        _currentEncounterThing = Instantiate(obj, spawnPos, Quaternion.identity);
    }

    public void StartDialogueEncounter(BaseCharacter thing)
    {
        Root.GetComponentFromRoot<UIHandler>().BuildDialoguePanel(thing);
    }

    public void EndEncounter()
    {
        _playerChar.HasEnteredEncounter = false;
        _playerChar.UsingUI = false;
        _playerChar.EncounteredThing = null;
        Destroy(_currentEncounterThing);
    }

    public void StartCombatEncounter(BaseEnemy enemy)
    {
        Debug.Log("starting Combat");
        Root.GetComponentFromRoot<UIHandler>().HideAllActiveUIPanels();
        Root.GetComponentFromRoot<UIHandler>().ShowCombatPanel();

        _currentEnemyInCombat = enemy;
        enemy.InitialiseThingForCombat(100.0f);
        enemy.CombatAI.inCombat = true;

        _playerInCombat = true;
        var hand = Root.GetComponentFromRoot<UIHandler>().CurrentActivePanel.GetComponent<PlayerHand>();
        _playerCombatHand = hand;
        //StartCoroutine("PlayerCombatLoop", hand);
        StartNewPlayerTurn(_playerCombatHand);
        

    }

    private void StartNewPlayerTurn(PlayerHand hand)
    {
        Debug.Log("starting new player turn");
        hand.Initialise();
        BeginPlayerTurn(hand);
        BeginEnemyStrategise(_currentEnemyInCombat);
    }

    IEnumerator PlayerCombatLoop(PlayerHand hand)
    {
        while (_playerInCombat)
        {
            // do turn timer here
            StartNewPlayerTurn(hand);
            yield return new WaitForSeconds(10.0f);
            Debug.Log("ending Player turn");
            PlayerEndTurn();
            BeginEnemyTurn();
        }

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
        _currentEnemyInCombat.CombatAI.InitiateMove();
        // TODO: each enemy type has its own moveset 
        StartNewPlayerTurn(_playerCombatHand);
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
        _playerCombatHand.DiscardAllCardsFromHand();
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
        _playerCombatHand.EndCardCrafting();
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
            _selectedCardsForCombination = _playerCombatHand.GetCardFromCraftingArea();

            var result = _cardCreator.CombineCards(_selectedCardsForCombination[0].GetComponent<Card>(), _selectedCardsForCombination[1].GetComponent<Card>());

            if(result != null)
            {
                _playerCombatHand.DiscardCardFromHand(_selectedCardsForCombination[0].GetComponent<Card>());
                _playerCombatHand.DiscardCardFromHand(_selectedCardsForCombination[1].GetComponent<Card>());
                Debug.Log(result.CardName);
                _playerCombatHand.AddCraftedCardToHand(result);
                QuitCardCraftingPhase();               
            }

        }
    }
    
}
