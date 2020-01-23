using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapPiece
{
    // will hold info for each map piece e.g. piece type, hallway, corner, end
    // e.g. if the piece has an encounter, or whether it has the final objective
    // maybe for other things like mesh spawn points eg torches
    EncounterType EncounterType
    {
        get;
        set;
    }

    void CheckTrigger();
}
