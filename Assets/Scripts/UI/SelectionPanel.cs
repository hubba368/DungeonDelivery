using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField]
    Button _button1;
    [SerializeField]
    Button _button2;
    [SerializeField]
    Button _button3;
    [SerializeField]
    Button _button4;
    [SerializeField]
    Button _button5;
    [SerializeField]
    Button _button6;

    public UISelectionPanelType PanelType;
    //private UnityAction _onButtonClickAction;

    private void Awake()
    {

    }

    public void InitPanel(UISelectionPanelType type)
    {
        _button1.gameObject.SetActive(false);
        _button2.gameObject.SetActive(false);
        _button3.gameObject.SetActive(false);
        _button4.gameObject.SetActive(false);
        _button5.gameObject.SetActive(false);
        _button6.gameObject.SetActive(false);

        switch (type)
        {
            case UISelectionPanelType.Generic:
                _button3.gameObject.SetActive(true);
                _button4.gameObject.SetActive(true);
                break;
            case UISelectionPanelType.Character:
                _button3.gameObject.SetActive(true);
                _button4.gameObject.SetActive(true);
                _button5.gameObject.SetActive(true);
                _button6.gameObject.SetActive(true);

                _button3.GetComponentInChildren<Text>().text = "Fight";
                _button4.GetComponentInChildren<Text>().text = "Talk";
                _button5.GetComponentInChildren<Text>().text = "Skill";
                _button6.GetComponentInChildren<Text>().text = "Flee";

                //TODO fight handler
                _button3.onClick.AddListener(()=> Root.GetComponentFromRoot<EncounterHandler>().StartCombatEncounter((BaseEnemy)Root.GetComponentFromRoot<UIHandler>().CurrentTargetThing));
                _button4.onClick.AddListener(()=> Root.GetComponentFromRoot<UIHandler>().BuildDialoguePanel(Root.GetComponentFromRoot<UIHandler>().CurrentTargetThing));
                _button5.onClick.AddListener(()=> Root.GetComponentFromRoot<UIHandler>().ShowSelectionPanel(UISelectionPanelType.Generic));
                // TODO flee mechanic
                //_button6.onClick.AddListener(_onButtonClickAction);
                break;
            case UISelectionPanelType.Item:
                _button3.gameObject.SetActive(true);
                _button4.gameObject.SetActive(true);

                _button3.GetComponentInChildren<Text>().text = "Take";
                _button4.GetComponentInChildren<Text>().text = "Leave";
                break;
        }
    }
}
