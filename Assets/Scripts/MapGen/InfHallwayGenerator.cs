using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfHallwayGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _currentHallwaySections;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    GameObject _hallwayPrefab;
    [SerializeField]
    GameObject _goalHallwayPrefab;

    private EncounterHandler _encounterHandler;

    public int InitHallwayPieces = 2;
    public int CurHallWayPieces = 0;
    public int MaxHallwayPieces = 5;
    public int NumOfMaxEncounters = 10;
    public int _encounterCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {
        _encounterHandler = this.GetComponent<EncounterHandler>();
        _playerTransform = Root.Instance._playerChar.transform;
        _currentHallwaySections = new List<GameObject>();
        InitHallway();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void InitHallway()
    {
        if(_playerTransform != null)
        {
           // AddHallwayPiece(new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z), true);
            float zPos = 0;

            for(int i = 0; i < InitHallwayPieces; i++)
            {
                AddHallwayPiece(new Vector3(_playerTransform.position.x, 0, zPos), true);
                zPos += 11.5f;
            }            
        }
    }

    void AddHallwayPiece(Vector3 offset, bool first = false)
    {
        CurHallWayPieces++;
        GameObject temp = Instantiate(_hallwayPrefab,
        offset,
        new Quaternion(0, 180, 0, 0));
        _currentHallwaySections.Add(temp);

        

        if (_encounterCount >= NumOfMaxEncounters)
        {
            CreateHallwayPiece(temp, 3);
        }
        else
        {
            CreateHallwayPiece(temp, Random.Range(0, 3));
        }
    }

    private void CreateHallwayPiece(GameObject temp, int encounterType)
    {
        if (encounterType != 0)
            ++_encounterCount;

        StraightHallway instance = temp.GetComponent<StraightHallway>();

        instance.HallwayOutOfRange += RemoveHallwayPiece;

        instance.EncounterType = (EncounterType)encounterType;
        Debug.Log(instance.EncounterType);

        instance.EncounterBegin += _encounterHandler.StartEncounter;
    }

    private void CreateGoalPiece(Vector3 offset)
    {
        CurHallWayPieces++;
        var hallway = Instantiate(_goalHallwayPrefab, offset, new Quaternion(0, 180, 0, 0));
        _currentHallwaySections.Add(hallway);
       // Root.GetComponentFromRoot<EncounterHandler>().StartGoalEncounter();
    }

    void RemoveHallwayPiece(GameObject item)
    {
        //Debug.Log("removing " + item.name);
        _currentHallwaySections.Remove(item);
        CurHallWayPieces--;
        if (CurHallWayPieces < MaxHallwayPieces)
        {
            AddHallwayPiece(new Vector3(_currentHallwaySections[_currentHallwaySections.Count - 1].transform.position.x, 0, (_currentHallwaySections[_currentHallwaySections.Count - 1].transform.position.z + 11.5f)));
        }
    }
}
