using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTriggerBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponentInParent<IMapPiece>().CheckTrigger();
        //this.GetComponent<BoxCollider>().enabled = false;
        if(this.GetComponentInParent<IMapPiece>().EncounterType == EncounterType.Character ||
            this.GetComponentInParent<IMapPiece>().EncounterType == EncounterType.Item)
        {
            
            other.GetComponent<PlayerController>().HasEnteredEncounter = true;
        }

    }

    void OnTriggerExit()
    {
        Debug.Log("leaving trigger");
        Root.GetComponentFromRoot<EncounterHandler>().EndEncounter();
        Root.GetComponentFromRoot<UIHandler>().HideAllActiveUIPanels();
    }
}
