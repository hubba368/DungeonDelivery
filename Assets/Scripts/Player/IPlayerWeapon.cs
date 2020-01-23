using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayerWeapon
{
    int DamageValue
    {
        get;
        set;
    }

    void OnAttackEvent();

    Collider AttackHitBox
    {
        get;
        set;
    }
}
