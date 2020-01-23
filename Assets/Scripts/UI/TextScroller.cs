using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroller : MonoBehaviour
{
    [SerializeField]
    private string _currentText;
    private List<char> _currentCharList;
    private int _currentIndex;

    public string CurrentText
    {
        get
        {
            return _currentText;
        }

        set
        {
            _currentText = value;
        }
    }





}
