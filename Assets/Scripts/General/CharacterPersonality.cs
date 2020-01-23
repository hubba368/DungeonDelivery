using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPersonality
{
    /*public static readonly CharacterPersonality HOSTILE = new CharacterPersonality
        (Root.GetComponentFromRoot<CharacterDialogueJsonParser>().GetGeneralDialogue(CharacterPersonalityType.Hostile),
        Root.GetComponentFromRoot<CharacterDialogueJsonParser>().GetCombatDialogue(CharacterPersonalityType.Hostile));
*/
    

    public static readonly CharacterPersonality NONHOSTILE = new CharacterPersonality
        (Root.GetComponentFromRoot<CharacterDialogueJsonParser>().GetGeneralDialogue(CharacterPersonalityType.Nonhostile),
        Root.GetComponentFromRoot<CharacterDialogueJsonParser>().GetCombatDialogue(CharacterPersonalityType.Nonhostile));

    private readonly List<string> _generalDialogueLines;
    private readonly List<string> _combatDialogueLines;

    public List<string> GeneralDialogueLines
    {
        get
        {
            return _generalDialogueLines;
        }
    }
    public List<string> CombatDialogueLines
    {
        get
        {
            return _combatDialogueLines;
        }
    }
    
    private CharacterPersonality(List<string> generalLines, List<string> combatLines)
    {
        this._generalDialogueLines = generalLines;
        this._combatDialogueLines = combatLines;
    }
}
