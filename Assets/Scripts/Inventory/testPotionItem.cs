using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPotionItem : BaseItem
{
    [SerializeField]
    Sprite _testImage;
    [SerializeField]
    ThingInfo _itemInfo;

    void Start ()
    {
        InitialiseThing(_itemInfo.name,
            _itemInfo.ThingDescription,
            _itemInfo.ThingImages,
            ThingType.Item, _itemInfo);
	}
	

}
