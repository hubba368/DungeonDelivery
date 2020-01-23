using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public static Root Instance { get; private set; }

    /*public UIHandler _uiHandler;
    public EncounterHandler _encounterHandler;
    public PizzaGenerator _pizzaGenerator;*/
    public PlayerController _playerChar;

    private Dictionary<Type, Component> _instances = new Dictionary<Type, Component>();

    // safer to store persistant classes in private cache instead of global vars.
    public static T GetComponentFromRoot<T>() where T : Component
    {
        if (Instance._instances.ContainsKey(typeof(T)))
        {
            return (T)Instance._instances[typeof(T)];
        }

        T temp = Instance.GetComponent<T>();
        if(temp == null)
        {
            Debug.Log("component not found on root");
            throw new Exception();
        }

        Instance._instances.Add(typeof(T), temp);
        return temp;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        GetPlayerChar();
    }

    private void GetPlayerChar()
    {
        _playerChar = GameObject.Find("PlayerChar").GetComponent<PlayerController>();
    }

    
}
