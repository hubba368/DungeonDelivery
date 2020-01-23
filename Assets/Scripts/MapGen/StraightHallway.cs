using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void RangeDelegate(GameObject item);
public delegate void EncounterDelegate(EncounterType type, Vector3 spawnPos);

public class StraightHallway : MonoBehaviour, IMapPiece
{
    [SerializeField]
    Transform _playerTransform;
    [SerializeField]
    float _currDistance;
    [SerializeField]
    float _targetDistance = 50f;
    [SerializeField]
    EncounterType _encounterType = 0;
    [SerializeField]
    Transform _spawnPoint;

    public EncounterType EncounterType
    {
        get { return _encounterType; }
        set { _encounterType = value; }
    }



    public event EncounterDelegate EncounterBegin;
    public event RangeDelegate HallwayOutOfRange;   

    void Start ()
    {
        _playerTransform = GameObject.Find("PlayerChar").transform;
    }
	

	void FixedUpdate ()
    {
        // for now, each hallway calcs distance from player, then it will tell generator to remove itself from the list.
        Vector3 distVector = _playerTransform.position - this.gameObject.transform.position;
        _currDistance = Vector3.Magnitude(distVector);
        if(_currDistance >= _targetDistance)
        {
            //Debug.Log("out of range");
            HallwayOutOfRange.Invoke(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void IMapPiece.CheckTrigger()
    {
        Vector3 temp = _spawnPoint.position + new Vector3(0, 1.0f);

        EncounterBegin.Invoke(_encounterType, temp);

    }
}
