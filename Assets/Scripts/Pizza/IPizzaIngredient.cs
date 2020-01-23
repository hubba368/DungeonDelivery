using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPizzaIngredient
{
    string IngredientName
    {
        get;
        set;
    }

    string IngredientNameFormatted
    {
        get;
        set;
    }



    FoodType IngredientFoodType
    {
        get;
    }

    Rarity IngredientRarity
    {
        get;
    }
}
