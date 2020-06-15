using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity { Common = 0, Uncommon = 1, Rare = 2, Legendary = 3 };

public enum FoodType { Meaty = 0, Veggie = 1 };

public enum EncounterType { Nothing = 0, Item = 1, Character = 2, Goal = 3 };

public enum ThingType { Character = 0, Item = 1 };

public enum UIPanelType { Debug = 0, Dialogue = 1, Menu = 2, PlayerMenu = 3, PlayerInteract = 4, PlayerSelection = 5 };

public enum UISelectionPanelType { Generic = 0, Character = 1, Item = 2 };

public enum StateID { Null = 0 };

public enum CharacterPersonalityType { Hostile = 0, Nonhostile = 1 };


public enum CardPotentialTarget { Debug = 0, Enemy = 1, Player = 2 };

public enum CardPotentialState
{
    Debug = 0,
    OnDraw = 1,
    OnUse = 2,
    OnEndTurn = 3,
    OnNewTurn = 4,
};

public enum CardEffectType
{
// Card effects will be actual 'spell' cards i.e. they will have their own functionality

    Debug = 0,
    Burning = 1, // DoT
    Bleeding = 2, // DoT
    Dazed = 3, // skip turns
    DrawCard = 4,
    DiscardCard = 5,
    ChangeCardCost = 6,
    CardDiscovery = 7, // choose X card of X type
    Block = 8,
    Confusion = 9, // target attacks itself
    Barrier = 10, // negate next attack
    ShuffleNewCardsIntoDeck = 11, // shuffle X cards into deck for current encounter
    Gain = 12, // gain some kind of buff/mechanic
    Weaken = 13, // Increase target damage by X multiplier
    CheckAllCardTypesInHand = 14, // will return bool whether all cards are of specific type
    CheckIfACardTypeInHand = 15,
    DrawFromDiscardPile = 16,
    ChooseCardInHandRandom = 17,
    Unplayable = 18, // will go with curse/status cards 
    CheckIfTargetHasEffect = 19, // will base its effect on whether its required effect exists in current state
    MultiplyAttribute = 20 // will be able to change diff attribs e.g. health, dmg, armour
};

/*Keywords (card additions that can change or enhance their func)
Curse = stays in hand until it is removed
Flimsy = depletes if not used when in hand
Save = card is not discarded on turn end
Scavenge = gets new action/item cards for craft phase
*/
public enum CardKeyword
{
    Debug = 0,
    Innate = 1, //always in first hand (can have max 4 if combo card)
    Deplete = 2, //Remove until end of combat
    Save = 3,
    Curse = 4,
    Flimsy = 5,
    Scavenge = 6,

};