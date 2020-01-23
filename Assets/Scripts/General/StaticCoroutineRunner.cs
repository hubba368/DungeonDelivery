using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for running coroutine behaviour on classes that dont derive from monobehaviour
public static class StaticCoroutineRunner
{
    private class Holder : MonoBehaviour { }

    private static Holder _cRunner;
    private static Holder CRunner
    {
        get
        {
            if(_cRunner == null)
            {
                _cRunner = new GameObject("Static Coroutine Helper").AddComponent<Holder>();
            }
            return _cRunner;
        }
    }

    public static void StartCoroutine(IEnumerator coroutine)
    {
        CRunner.StartCoroutine(coroutine);
    }
}
