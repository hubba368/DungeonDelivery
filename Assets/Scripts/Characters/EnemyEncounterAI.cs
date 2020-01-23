using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterAI : CombatFSM
{
    private List<IState> _allStates = new List<IState>();

    IState IdleState;
    IState AttackState;
    IState DeathState;

    bool attacking = false;
    bool isDead = false;
    bool isEnemyTurn = false;
    public bool hasFoundPotentialMove = false;
    public bool inCombat = false; // add property

    private List<CardInfo> _enemyHand;
    private Stack<CardInfo> _enemyDeck;

    private int MaxEnemyHandCount = 4;
    private int MaxEnemyMana = 5;
    private int _currentEnemyMana = 0;

    public List<CardInfo> EnemyHand
    {
        get
        {
            return _enemyHand;
        }

        set
        {
            _enemyHand = value;
        }
    }

    public void InitFSM()
    {
       /* Object[] temp = (Resources.LoadAll("ScriptableObjs/Things/Cards/EnemyCards", typeof(CardInfo)));
        for (int i = 0; i < 30; ++i)
        {
            var t = (CardInfo)temp[i];
            _enemyDeck.Push(t);
        }*/

        IdleState = new EnemyIdleState();
        AttackState = new EnemyAttackState();
        DeathState = new EnemyDeathState();

        _allStates.Add(IdleState);
        _allStates.Add(AttackState);
        _allStates.Add(DeathState);

        IdleState.SetParent(this.GetComponent<BaseEnemy>());
        AttackState.SetParent(this.GetComponent<BaseEnemy>());
        DeathState.SetParent(this.GetComponent<BaseEnemy>());


        //StartCoroutine("UpdateFSM");
    }
	
	void Update ()
    {
        if (isDead)
        {
            ChangeState(DeathState);
            //TODO: do death anim
            Destroy(gameObject);
        }
        if (hasFoundPotentialMove)
        {
            // TODO: show move icon (attack icon, defence icon etc)
            // and wait for player to end their turn
        }
    }

    public void InitiateMove()
    {
        // TODO: enemy will perform its chosen move (if any) anims and all
        Debug.Log("enemy is performing its chosen attack");
    }

    public void InitiateThink()
    {
        // TODO: enemy will check for combos based on its current hand
        ChangeState(IdleState);
        CurrentState.Execute();
    }

    // might not need this if going for non real time gameplay :(
    IEnumerator UpdateFSM()
    {
        while (!isDead && !isEnemyTurn)
        {
            if (CurrentState != null)
            {
                // TODO overhaul this to check for card usage instead of mana count initially
                if (attacking)
                {
                    // TODO: do attack anims and stuff

                }

                CurrentState.Execute();
                yield return new WaitForSeconds(1.0f);
                ChangeState(IdleState);
                attacking = false;
            }
        }      
    }

    public void DrawCardFromDeck()
    {
        if (_enemyHand.Count >= MaxEnemyHandCount)
            return;

        CardInfo card = null;
        if (_enemyDeck.Count > 0)
        {
            card = _enemyDeck.Pop();
            _enemyHand.Add(card);
        }
        else
        {
            return;
        }
    }

    public void GainMana()
    {
        if (_currentEnemyMana >= MaxEnemyMana)
            return;

        ++_currentEnemyMana;
        // todo: update enemy mana UI
    }
}
