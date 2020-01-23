using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedPizza : IPizzaBase
{

    List<IPizzaIngredient> _ingredientList;

    string _pizzaFullName;

    public List<IPizzaIngredient> IngredientList
    {
        get
        {
            return _ingredientList;
        }

        set
        {
            _ingredientList = value;
        }
    }

    public string PizzaFullName
    {
        get
        {
            return _pizzaFullName;
        }
    }

    public CraftedPizza()
    {

    }

    public CraftedPizza(string name, List<IPizzaIngredient> ing)
    {
        _ingredientList = ing;
        _pizzaFullName = name;
    }
}
