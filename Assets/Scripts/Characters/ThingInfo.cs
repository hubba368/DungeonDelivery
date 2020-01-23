using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Things", menuName = "Things/InGameThing")]
public class ThingInfo : ScriptableObject
{
    [SerializeField]
    private string _thingName;
    [SerializeField]
    private string _thingDescription;

    [Tooltip("Images that are used for UI.")]
    [SerializeField]
    private List<Sprite> _thingImages;

    [Tooltip("In-game sprite of the thing.")]
    [SerializeField]
    private Sprite _thingSprite;

    [Tooltip("List of names that a given object can have, can either be a handcrafted list or auto generated.")]
    [SerializeField]
    private List<string> _characterNames;

    [Tooltip("List of dialogue lines for general use, such as initial interaction with a character.")]
    [SerializeField]
    private List<string> _generalDialogueLines;

    [Tooltip("List of dialogue lines for use within combat, such as 'taunts' or telegraphed lines for attacks.")]
    [SerializeField]
    private List<string> _combatDialogueLines;

    public string ThingName
    {
        get
        {
            return _thingName;
        }

        set
        {
            _thingName = value;
        }
    }

    public string ThingDescription
    {
        get
        {
            return _thingDescription;
        }

        set
        {
            _thingDescription = value;
        }
    }

    public List<Sprite> ThingImages
    {
        get
        {
            return _thingImages;
        }

        set
        {
            _thingImages = value;
        }
    }

    public List<string> CharacterNames
    {
        get
        {
            return _characterNames;
        }

        set
        {
            _characterNames = value;
        }
    }

    public List<string> GeneralDialogueLines
    {
        get
        {
            return _generalDialogueLines;
        }

        set
        {
            _generalDialogueLines = value;
        }
    }

    public List<string> CombatDialogueLines
    {
        get
        {
            return _combatDialogueLines;
        }

        set
        {
            _combatDialogueLines = value;
        }
    }

    public Sprite ThingSprite
    {
        get
        {
            return _thingSprite;
        }

        set
        {
            _thingSprite = value;
        }
    }

    public void GenerateName()
    {
        int random = Random.Range(0, CharacterNames.Count);
        ThingName = CharacterNames[random];
    }

}
