using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ShowPanelDelegate(UIPanelType type);

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _currentActivePanel;
    [SerializeField]
    GameObject _dialoguePanel;
    [SerializeField]
    GameObject _interactPanel;
    [SerializeField]
    GameObject _debugPanel;
    [SerializeField]
    GameObject _menuPanel;
    [SerializeField]
    GameObject _playerMenuPanel;
    [SerializeField]
    GameObject _combatPanel;
    [SerializeField]
    Canvas _canvas;

    public GameObject _selectionPanel;

    private BaseCharacter _currentTargetThing;

    public BaseCharacter CurrentTargetThing
    {
        get
        {
            return _currentTargetThing;
        }

        set
        {
            _currentTargetThing = value;
        }
    }

    public GameObject CurrentActivePanel
    {
        get
        {
            return _currentActivePanel;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _currentActivePanel = null;
        
    }

    public void ShowUIPanelGeneric(UIPanelType type)
    {
        GameObject temp = null;
        switch (type)
        {
            case UIPanelType.Debug:
                break;
            case UIPanelType.Dialogue:
                temp = Instantiate(_dialoguePanel);
                temp.transform.SetParent(_canvas.transform, false);
                break;
            case UIPanelType.Menu:
                break;
            case UIPanelType.PlayerMenu:
                break;
            case UIPanelType.PlayerInteract:
                temp = Instantiate(_interactPanel);
                temp.transform.SetParent(_canvas.transform, false);
                break;
        }
        _currentActivePanel = temp;
        CurrentActivePanel.SetActive(true);
    }

    // TODO: maybe make this private and change it so the class sends an event maybe to get the Thing param?
    public void BuildDialoguePanel(BaseCharacter thing)
    {
        _currentTargetThing = thing;
        HideUIPanel();

        var temp = Instantiate(_dialoguePanel);
        temp.transform.SetParent(_canvas.transform, false);
        DialoguePanel newPanel = temp.GetComponent<DialoguePanel>();
        newPanel.InitDialoguePanel(thing);
        UpdateCurrentPanel(newPanel.gameObject);
    }

    public void ShowSelectionPanel(UISelectionPanelType type)
    {
        HideUIPanel();
        switch (type)
        {
            case UISelectionPanelType.Character:
                var temp = Instantiate(_selectionPanel);
                temp.transform.SetParent(_canvas.transform, false);
                SelectionPanel newPanel = temp.GetComponent<SelectionPanel>();
                newPanel.InitPanel(type);
                UpdateCurrentPanel(newPanel.gameObject);
                break;
        }
    }

    public void ShowCombatPanel()
    {
        //HideUIPanel();
        var temp = Instantiate(_combatPanel);
        temp.transform.SetParent(_canvas.transform, false);
        UpdateCurrentPanel(temp);
    }

    public void HideUIPanel()
    {
        // TODO move this to seperate 'close all panels' func.
        /*if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/

        //CurrentTargetThing = null;

        if(CurrentActivePanel != null)
        {
            CurrentActivePanel.SetActive(false);
            Destroy(CurrentActivePanel.gameObject);
        }
        else
        {
            Debug.Log("active panel is null");
        }

    }

    private void UpdateCurrentPanel(GameObject obj)
    {
        _currentActivePanel = obj;
    }

    public void HideAllActiveUIPanels()
    {
        CurrentTargetThing = null;
        for(int i = 0; i < _canvas.transform.childCount; ++i)
        {
            Destroy(_canvas.transform.GetChild(i).gameObject);
        }
    }
}
