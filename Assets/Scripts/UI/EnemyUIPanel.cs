using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIPanel : MonoBehaviour
{
    [SerializeField]
    Text _currentEnemyHealth;
    [SerializeField]
    Image _currentEnemyNextAction;

    public void UpdateEnemyHealth(int amount)
    {
        _currentEnemyHealth.text = amount.ToString();
    }

    // TODO: update this to use some kind of 'enemyaction' class/identifier
    public void UpdateNextEnemyAction(Sprite nextAction)
    {
        _currentEnemyNextAction.sprite = nextAction;
    }
}
