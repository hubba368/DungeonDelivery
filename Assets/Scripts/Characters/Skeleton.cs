using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BaseEnemy
{
    [SerializeField]
    Sprite testImage;
    [SerializeField]
    ThingInfo charInfo;

	void Start ()
    {
        charInfo.GenerateName();
        //charInfo.GeneralDialogueLines = CharacterPersonality.NONHOSTILE.GeneralDialogueLines;
        // can either use scriptable obj lines or grab lines from json
        this.InitialiseThing("Skeleton",
            charInfo.ThingDescription,
            charInfo.ThingName,
            charInfo.ThingImages, ThingType.Character, charInfo);

        GetComponent<SpriteRenderer>().sprite = charInfo.ThingSprite;

        Debug.Log(charInfo.ThingName + " + " + charInfo.ThingDescription);
	}

}
