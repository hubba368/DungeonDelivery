using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPizzaBase
{
    List<IPizzaIngredient> IngredientList
    {
        get;
        set;
    }

    string PizzaFullName
    {
        get;
    }
}
