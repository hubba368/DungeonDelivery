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

    CombatHandler _combatHandler;


    bool _playerInCombat = false;




    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerChar = Root.Instance._playerChar;
        
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

        _playerInCombat = true;
        var hand = Root.GetComponentFromRoot<UIHandler>().CurrentActivePanel.GetComponent<PlayerHand>();
        var obj = this.gameObject.AddComponent<CombatHandler>();
        var cardCreator = Instantiate(_comboCardCreatorPrefab).GetComponent<ComboCardCreator>();
        _combatHandler = obj;
        _combatHandler.StartCombatEncounter(enemy, hand, cardCreator);

    }

    public void EndCombatEncounter()
    {
        if (_combatHandler)
        {
            _combatHandler.EndCombatEncounter();
            _combatHandler = null;
            _playerInCombat = false;
            EndEncounter();
        }
    }


    /*
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

    }*/

    

    
    
}
