using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour, IPlayerWeapon
{
    private int _damageValue;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Animation _attackAnim;

    private GameObject _parentObj;

    [SerializeField]
    private Collider _attackHitBox;


    public GameObject ParentObj
    {
        get
        {
            return _parentObj;
        }

        set
        {
            _parentObj = value;
        }
    }

    public int DamageValue
    {
        get
        {
            return _damageValue;
        }

        set
        {
            _damageValue = value;
        }
    }

    public Collider AttackHitBox
    {
        get
        {
            return _attackHitBox;
        }

        set
        {
            _attackHitBox = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        _anim = this.gameObject.GetComponent<Animator>();
        _attackAnim = this.gameObject.GetComponent<Animation>();
        _attackHitBox = this.gameObject.GetComponent<BoxCollider>();
        _attackHitBox.enabled = false;
	}

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            _attackHitBox.enabled = false;
        }
        else
        {
            Debug.Log("No hit");
            _attackHitBox.enabled = false;
        }
    }

    public void OnAttackEvent()
    {
        _anim.SetTrigger("AttackTrigger");
        _attackHitBox.enabled = true;
    }

    void ResetAttackTrigger()
    {
        _attackHitBox.enabled = false;
    }
}
