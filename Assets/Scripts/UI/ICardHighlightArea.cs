using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardHighlightArea
{
    void OnClickArea();

    bool CompareCardInArea(CardInfo card);

}
