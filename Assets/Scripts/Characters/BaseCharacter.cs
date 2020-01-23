using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseCharacter
{

    string Name
    {
        get;
    }

    string Description
    {
        get;
    }

    string CharacterName
    {
        get;
    }

    List<Sprite> CharacterImages
    {
        get;
    }

    ThingType ThingType
    {
        get;
    }

    ThingInfo ThingInfo
    {
        get;
    }
}
