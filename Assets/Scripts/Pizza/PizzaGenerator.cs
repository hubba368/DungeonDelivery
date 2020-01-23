using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class PizzaGenerator : MonoBehaviour
{
    [SerializeField]
    int MaxIngredientCount = 3;
    [SerializeField]
    bool GenerateFullPizzaName = false;
    [SerializeField]
    TextAsset MeatPrefixesFile;
    [SerializeField]
    TextAsset VegPrefixesFile;
    [SerializeField]
    TextAsset MeatSuffixesFile;
    [SerializeField]
    TextAsset VegSuffixesFile;

    PizzaNameDefs pizzaDefs;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start ()
    {
        InitAllPizzaInfo();

    }
	
	void InitAllPizzaInfo()
    {
        // get pizza things from json file
        pizzaDefs = new PizzaNameDefs();
        //TextAsset jsonFile = Resources.Load<TextAsset>("/Text/PizzaJson");
        //Debug.Log(jsonFile);
        pizzaDefs.MeatPrefixes = JsonConvert.DeserializeObject<List<string>>(MeatPrefixesFile.text);
        pizzaDefs.MeatSuffixes = JsonConvert.DeserializeObject<List<string>>(MeatSuffixesFile.text);
        pizzaDefs.VegPrefixes = JsonConvert.DeserializeObject<List<string>>(VegPrefixesFile.text);
        pizzaDefs.VegSuffixes = JsonConvert.DeserializeObject<List<string>>(VegSuffixesFile.text);
    }

    public void AssemblePizza(List<IPizzaIngredient> ingredients)
    {
        // Extra TODO: food types can determine end product
        CraftedPizza newPizza = new CraftedPizza();
        string finalName = "";
        int mostCommonFood = GetMostCommonFoodType(ingredients);
        int randVal = 0;

        finalName += ingredients.Max(item => item.IngredientRarity).ToString() + " ";
        
        if(mostCommonFood == 0)
        {
            randVal = Random.Range(0, pizzaDefs.MeatPrefixes.Count() - 1);
            finalName += pizzaDefs.MeatPrefixes[randVal] + " ";
        }
        else
        {
            randVal = Random.Range(0, pizzaDefs.VegPrefixes.Count() - 1);
            finalName += pizzaDefs.VegPrefixes[randVal] + " ";
        }  

        if (GenerateFullPizzaName)
        {
            finalName += GenerateNameFromIngredients(ingredients);
        }

        if(Random.Range(0, 2) == 0)
        {
            randVal = Random.Range(0, pizzaDefs.MeatSuffixes.Count() - 1);
            finalName += pizzaDefs.MeatSuffixes[randVal] + " ";
        }
        else
        {
            randVal = Random.Range(0, pizzaDefs.VegSuffixes.Count() - 1);
            finalName += pizzaDefs.VegSuffixes[randVal] + " ";
        }

        Debug.Log(finalName);
    }

    string GenerateNameFromIngredients(List<IPizzaIngredient> ings)
    {
        string name = "";

        int counter = 0;

        foreach(IPizzaIngredient p in ings)
        {
            if(counter == ings.Count)
            {
                name += p.IngredientNameFormatted + " ";
            }
            counter++;
            name += p.IngredientNameFormatted + " and ";
        }

        return name;
    }


    int GetMostCommonFoodType(List<IPizzaIngredient> ingredients)
    {
        int meatCount = 0;
        int vegCount = 0;

        foreach(IPizzaIngredient p in ingredients)
        {
            if(p.IngredientFoodType == FoodType.Meaty)
            {
                meatCount++;
            }
            else
            {
                vegCount++;
            }
        }

        if(meatCount > vegCount)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
