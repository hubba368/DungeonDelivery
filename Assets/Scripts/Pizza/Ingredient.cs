using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IPizzaIngredient
{
    string _ingredientName;
    string _ingredientNameFormatted;
    FoodType _foodType;
    Rarity _rarity;

    public string IngredientName
    {
        get
        {
            return _ingredientName;
        }

        set
        {
            _ingredientName = value;
        }
    }
    public string IngredientNameFormatted
    {
        get
        {
            return _ingredientNameFormatted;
        }
        set
        {
            _ingredientNameFormatted = value;
        }
    }
    public FoodType IngredientFoodType
    {
        get
        {
            return _foodType;
        }
    }
    public Rarity IngredientRarity
    {
        get
        {
            return _rarity;
        }
    }

    public Ingredient(string name, string nameF, FoodType fType, Rarity rType)
    {
        _ingredientName = name;
        _ingredientNameFormatted = nameF;
        _foodType = fType;
        _rarity = rType;
    }
}
