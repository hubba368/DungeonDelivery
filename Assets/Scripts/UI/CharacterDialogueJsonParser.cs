using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public class CharacterDialogueJsonParser : MonoBehaviour
{
    // this is very inextensible but dont really need to extend for now
    [SerializeField]
    private List<string> _nonHostileGenDialogue;
    private List<string> _hostileGenDialogue;
    private List<string> _nonHostileCbtDialogue;
    private List<string> _hostileCbtDialogue;

    [SerializeField]
    TextAsset nonHostileGen;
    [SerializeField]
    TextAsset nonHostileCbt;
    [SerializeField]
    TextAsset hostileGen;
    [SerializeField]
    TextAsset hostileCbt;

    private void Awake()
    {
        DontDestroyOnLoad(this);

       /* _nonHostileGenDialogue = JsonConvert.DeserializeObject<List<string>>(nonHostileGen.text);
        _hostileGenDialogue = JsonConvert.DeserializeObject<List<string>>(hostileGen.text);
        _nonHostileCbtDialogue = JsonConvert.DeserializeObject<List<string>>(nonHostileCbt.text);
        _hostileCbtDialogue = JsonConvert.DeserializeObject<List<string>>(hostileCbt.text);*/
    }

    public List<string> GetGeneralDialogue(CharacterPersonalityType type)
    {
        List<string> temp = new List<string>();
        switch (type)
        {
            case CharacterPersonalityType.Hostile:
                temp = _hostileGenDialogue;
                break;
            case CharacterPersonalityType.Nonhostile:
                temp = _nonHostileGenDialogue;
                break;

        }
        return temp;
    }
    public List<string> GetCombatDialogue(CharacterPersonalityType type)
    {
        List<string> temp = new List<string>();
        switch (type)
        {
            case CharacterPersonalityType.Hostile:
                temp = _hostileCbtDialogue;
                break;
            case CharacterPersonalityType.Nonhostile:
                temp = _nonHostileCbtDialogue;
                break;

        }
        return temp;
    }
}
