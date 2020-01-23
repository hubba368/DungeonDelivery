using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialoguePanel : MonoBehaviour
{
    [SerializeField]
    string _thingName;
    [SerializeField]
    string _currentDialoguePlaceholderText;
    [SerializeField]
    string _currentDialogueText;
    [SerializeField]
    Image _currentPortrait;
    [SerializeField]
    Text _textField;
    [SerializeField]
    Text _nameField;
    [SerializeField]
    Button _continueDialogueButton;
    [SerializeField]
    Button _takeItemButton;
    [SerializeField]
    Button _leaveItemButton;
    [SerializeField]
    Text _itemDialogueField;

    Dictionary<string, List<Transform>> UIElements = new Dictionary<string, List<Transform>>();

    List<Transform> _characterUIElements = new List<Transform>();
    List<Transform> _itemUIElements = new List<Transform>();

    int _currentDialogueIndex;
    int _currentDialogueLineIndex = 0;

    BaseCharacter _currentThing;
    BaseEnemy _currentCharacter;

    bool _dialogueFinishedScrolling = false;
    bool _isCurrentTargetAnItem = false;

    public bool _dialogueComplete = false;

    private UnityAction _onContinueDialogueAction;
    private UnityAction _onTakeItemAction;
    private UnityAction _onLeaveItemAction;

    List<string> _dialogueLines;
    string _currentDialogueLine;

    public BaseEnemy CurrentCharacter
    {
        set
        {
            _currentCharacter = value;
        }
    }

    private void Awake()
    {
        _characterUIElements.Add( _currentPortrait.transform);
        _characterUIElements.Add(_nameField.transform);
        _characterUIElements.Add( _continueDialogueButton.transform);
        _characterUIElements.Add(_textField.transform);
        _itemUIElements.Add(_takeItemButton.transform);
        _itemUIElements.Add( _leaveItemButton.transform);
        _itemUIElements.Add( _itemDialogueField.transform);

        UIElements.Add("Character", _characterUIElements);
        UIElements.Add("Item", _itemUIElements);

        SetUIVisibility(false);
        Cursor.visible = true;

        _onContinueDialogueAction += OnContinueDialogue;
        _continueDialogueButton.onClick.AddListener(_onContinueDialogueAction);

        _onTakeItemAction += OnTakeItem;
        _takeItemButton.onClick.AddListener(_onTakeItemAction);

        _onLeaveItemAction += OnLeaveItem;
        _leaveItemButton.onClick.AddListener(_onLeaveItemAction);
        _dialogueLines = new List<string>();
        
        
    }

    public void InitDialoguePanel(BaseCharacter thing)
    {
        _currentThing = thing;

        switch (thing.ThingType)
        {
            case ThingType.Character:
                SetUIVisibility(true, "Character");
                _dialogueLines = thing.ThingInfo.GeneralDialogueLines;
                Debug.Log(_dialogueLines.Count);
                _thingName = thing.CharacterName;
                _currentPortrait.sprite = thing.CharacterImages[0];
                _nameField.text = _thingName;
                _currentDialoguePlaceholderText = _dialogueLines[_currentDialogueLineIndex];
                _isCurrentTargetAnItem = false;
                break;
            case ThingType.Item:               
                SetUIVisibility(true, "Item");
                _currentDialoguePlaceholderText = "You find a " + thing.Name + ". Will you take it?";
                _isCurrentTargetAnItem = true;
                break;
        }

        _textField.text = _currentDialogueText;
  
        StartCoroutine("ScrollText");
    }

    public void OnTakeItem()
    {
        Debug.Log("taking item");
        Root.GetComponentFromRoot<UIHandler>().HideAllActiveUIPanels();
        Root.GetComponentFromRoot<EncounterHandler>().EndEncounter();
    }

    public void OnLeaveItem()
    {
        Debug.Log("leaving item");
        Root.GetComponentFromRoot<UIHandler>().HideAllActiveUIPanels();
        Root.GetComponentFromRoot<EncounterHandler>().EndEncounter();
    }

    public void OnContinueDialogue()
    {
        if (_dialogueComplete)
        {
            // tell ui handler to make selection panel if needed
            // will probs phase this out for card UI
            switch (_currentThing.ThingType)
            {
                case ThingType.Character:
                    Root.GetComponentFromRoot<UIHandler>().ShowSelectionPanel(UISelectionPanelType.Character);
                    break;
            }
        }
        
        if (_dialogueFinishedScrolling)
        {
            
            if(_currentDialogueLineIndex >= _dialogueLines.Count-1)
            {
                _dialogueComplete = true;
            }
            else
            {
                _currentDialogueLineIndex++;
                UpdateDialogue(GetNextDialogueLine(_currentDialogueLineIndex));
                StartCoroutine("ScrollText");
            }
        }
    }

    string GetNextDialogueLine(int index)
    {
        string t = "";
        if(_dialogueLines[index] != null)
        {
            t = _dialogueLines[index];
        }
        return t;
    }

    IEnumerator ScrollText()
    {
        while (_currentDialogueIndex < _currentDialoguePlaceholderText.Length)
        {
            _currentDialogueText += _currentDialoguePlaceholderText[_currentDialogueIndex];
            _currentDialogueIndex++;

            if (_isCurrentTargetAnItem)
            {
                _itemDialogueField.text = _currentDialogueText;
            }
            else
            {
                _textField.text = _currentDialogueText;
            }
            
            yield return new WaitForSeconds(0.05f);           
        }
        _dialogueFinishedScrolling = true;
    }

    private void UpdateDialogue(string text)
    {
        _currentDialogueIndex = 0;
        _currentDialoguePlaceholderText = "";
        _currentDialogueText = "";
        _currentDialoguePlaceholderText = text;
    }

    private void SetUIVisibility(bool value, string tag = null)
    {
        if(tag == null)
        {
            foreach(var x in UIElements["Character"])
            {
                x.gameObject.SetActive(value);
            }
            foreach (var x in UIElements["Item"])
            {
                x.gameObject.SetActive(value);
            }
        }
        else
        {
            var tempList = UIElements[tag];
            for (int i = 0; i < tempList.Count(); i++)
            {
                tempList[i].gameObject.SetActive(value);
            }
        }
    }
}
