using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CardCraftingDatatable : MonoBehaviour
{
    private Dictionary<int, string> _craftingTable;

    [SerializeField]
    TextAsset _craftingTableFile;

    [SerializeField]
    CardInfo testCard;

    private void Awake()
    {
        DontDestroyOnLoad(this);

       // DeserialiseCraftingTableFromJSON();
    }


    public CardInfo GetRecipeByKey(int key)
    {
        CardInfo result = null;
        Debug.Log(key);
        var temp = testCard;
        result = temp;
        /*if (_craftingTable.ContainsKey(key))
        {
            result = temp;//_craftingTable[key];
        }*/
        return result;
    }

    private void DeserialiseCraftingTableFromJSON()
    {
        var tempDict = JsonConvert.DeserializeObject<Dictionary<int, string>>(_craftingTableFile.text);
        _craftingTable = new Dictionary<int, string>(tempDict);
    }
}
