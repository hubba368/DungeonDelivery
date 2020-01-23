using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseItem : MonoBehaviour, BaseCharacter
{
    private string _description;
    private string _itemName;
    private List<Sprite> _itemImages;
    private ThingType _itemThingType;
    private ThingInfo _thingInfo;

    public string Name
    {
        get
        {
            return _itemName;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }

    public string CharacterName
    {
        get
        {
            return _itemName;
        }
    }

    public List<Sprite> CharacterImages
    {
        get
        {
            return _itemImages;
        }
    }

    public ThingType ThingType
    {
        get
        {
            return _itemThingType;
        }
    }

    public ThingInfo ThingInfo
    {
        get
        {
            return _thingInfo;
        }
    }

    public void InitialiseThing(string name, string desc, List<Sprite> charImgs, ThingType thingType, ThingInfo info)
    {
        _itemName = name;
        _description = desc;
        _itemImages = charImgs;
        _itemThingType = thingType;
        _thingInfo = info;
    }
}
