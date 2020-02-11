using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCraftingArea : CardPlaceableArea
{

	// Use this for initialization
	void Start ()
    {
        _playerHand = this.transform.parent.parent.GetComponent<PlayerHand>();
        _areaButton.onClick.AddListener(OnClickArea);
	}

    public override void OnClickArea()
    {


        _playerHand.MoveCardToHighlightArea(this.transform);
    }

    public override bool CompareCardInArea(CardInfo card)
    {
        return base.CompareCardInArea(card);
    }
}
