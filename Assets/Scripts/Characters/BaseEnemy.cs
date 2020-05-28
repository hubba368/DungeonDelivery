using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, BaseCharacter
{
    private string _name;
    private string _description;
    private string _characterName;
    private float _characterHealth;
    private List<Sprite> _characterImages;
    private List<BaseCharacterEffect> _attachedCardEffects;
    private ThingType _thingType;
    private ThingInfo _thingInfo;

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    public string CharacterName
    {
        get
        {
            return _characterName;
        }
        set
        {
            _characterName = value;
        }
    }

    public List<Sprite> CharacterImages
    {
        get
        {
            return _characterImages;
        }
        set
        {
            _characterImages = value;
        }
    }

    public ThingType ThingType
    {
        get
        {
            return _thingType;
        }

        set
        {
            _thingType = value;
        }
    }

    public float CharacterHealth
    {
        get
        {
            return _characterHealth;
        }

        set
        {
            _characterHealth = value;
        }
    }

    public ThingInfo ThingInfo
    {
        get
        {
            return _thingInfo;
        }
    }

    public List<BaseCharacterEffect> AttachedCardEffects
    {
        get
        {
            return _attachedCardEffects;
        }
    }

    public EnemyEncounterAI CombatAI;

    public void InitialiseThing(string name, string desc, string charName, List<Sprite> charImgs, ThingType thingType, ThingInfo info)
    {
        Name = name;
        Description = desc;
        CharacterName = charName;
        CharacterImages = charImgs;
        ThingType = thingType;
        _thingInfo = info;
    }

    public void InitialiseThingForCombat(float health)
    {
        _characterHealth = health;
        CombatAI = gameObject.GetComponent<EnemyEncounterAI>();
        CombatAI.InitFSM();
    }

    public void InitialiseThingPersonality(CharacterPersonality type)
    {
        //GenDialogueLines = type.GeneralDialogueLines;
        //CbtDialogueLines = type.CombatDialogueLines;
    }

    public void AttachNewCardEffectToEnemy(BaseCharacterEffect effect)
    {
        // TODO: check if effect already exists?
        _attachedCardEffects.Add(effect);
    }
}
