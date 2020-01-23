using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 10.0f;
    [SerializeField]
    private float _LookSensitivity = 5.0f;
    [SerializeField]
    private float _LookSmoothing = 2.0f;

    private float _verticalMovement;
    private float _horizontalMovement;

    public float PlayerHealth = 100.0f;
    public int MaxPlayerMana = 5;
    public int CurrentPlayerMana = 0;

    private Vector2 _mouseLook;
    private Vector2 _smoothValue;
    [SerializeField]
    private GameObject _playerWeapon;
    [SerializeField]
    private GameObject _playerPizza;

    private IPlayerWeapon _playerWeaponController;

    private Camera _playerCamera;

    public bool UsingUI = false;

    public bool HasEnteredEncounter = false;
    public BaseCharacter EncounteredThing = null;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Confined;

        _playerCamera = this.GetComponentInChildren<Camera>();

        //_playerWeapon = Resources.Load<GameObject>("Prefabs/Player/PlayerSword");
        //_playerWeapon = Instantiate(_playerWeapon, this.transform.GetChild(0));
        //_playerWeapon.transform.localPosition = new Vector3(0.3f, -1, 1.3f);
        //_playerWeapon.SetActive(false);

        //_playerPizza = Resources.Load<GameObject>("Prefabs/Player/PlayerPizza");
        //_playerPizza = Instantiate(_playerPizza, this.transform.GetChild(0));
        //_playerPizza.transform.localPosition = new Vector3(-0.4f, -0.3f, 1.3f);
        //_playerPizza.SetActive(false);

        //_playerWeaponController = _playerWeapon.GetComponent<SwordBehaviour>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(PlayerHealth <= 0.0f)
        {
            Debug.Log("player is dead");
        }

        if (!HasEnteredEncounter)
        {
            if (Input.GetButtonDown("LeftMouseClick"))
            {
                /*_playerWeaponController.OnAttackEvent();

                Ingredient meatIng = new Ingredient("HumanEyeBall", "Eyeball", FoodType.Meaty, Rarity.Common);
                Ingredient meatIng2 = new Ingredient("DragonTail", "Dragon Tail", FoodType.Meaty, Rarity.Legendary);
                Ingredient vegIng = new Ingredient("Pepper", "Pepper", FoodType.Veggie, Rarity.Common);
                Ingredient vegIng2 = new Ingredient("CaveShroom", "Cave Shroom", FoodType.Veggie, Rarity.Uncommon);

                List<IPizzaIngredient> ings = new List<IPizzaIngredient>();
                ings.Add(meatIng);
                ings.Add(vegIng);
                ings.Add(vegIng2);

                Root.GetComponentFromRoot< PizzaGenerator>().AssemblePizza(ings);*/
            }
        }
        else
        {
            if (Input.GetButtonDown("LeftMouseClick"))
            {
                if(UsingUI == false)
                {

                    EncounteredThing = CheckForEncounterTargetThing();

                    if (EncounteredThing != null)
                    {
                        Cursor.visible = true;                     
                        UsingUI = true;
                        Root.GetComponentFromRoot<EncounterHandler>().StartDialogueEncounter(EncounteredThing);
                        
                    }
                }
                else
                {

                }
            }
        }
	}

    private void FixedUpdate()
    {
        if (!HasEnteredEncounter)
        {
            _verticalMovement = Input.GetAxis("Vertical") * _playerSpeed * Time.deltaTime;
            _horizontalMovement = Input.GetAxis("Horizontal") * _playerSpeed * Time.deltaTime;
            transform.Translate(_horizontalMovement, 0, _verticalMovement);

            // CameraUpdate();
        }

    }

    public void IncrementPlayerMana(int amount)
    {
        if (CurrentPlayerMana >= MaxPlayerMana)
            return;

        CurrentPlayerMana += amount;
    }

    void CameraUpdate()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(_LookSensitivity * _LookSmoothing, _LookSensitivity * _LookSmoothing));

        _smoothValue.x = Mathf.Lerp(_smoothValue.x, mouseDelta.x, 1f / _LookSmoothing);
        _smoothValue.y = Mathf.Lerp(_smoothValue.y, mouseDelta.y, 1f / _LookSmoothing);

        _mouseLook += _smoothValue;

        _playerCamera.transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, transform.up);
    }

    BaseCharacter CheckForEncounterTargetThing()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.localPosition, 100.0f, 1 << 9);
        return hitColliders[0].gameObject.GetComponent<BaseCharacter>();

    }

    void AttackWithWeapon()
    {
        if(_playerWeapon != null)
        {
            if(_playerWeapon.GetComponent<Animator>() != null)
            {
                
                //_playerWeaponController.BeginAttacking();
            }
            else
            {
                Debug.Log("error");
            }

        }
    }
}
