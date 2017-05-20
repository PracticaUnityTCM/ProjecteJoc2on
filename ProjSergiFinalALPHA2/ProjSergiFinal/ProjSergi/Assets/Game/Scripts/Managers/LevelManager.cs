using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }
    public GameObject Ship;
    public CameraController CameraController;
    public Checkpoint DebugSpwan;

    private List<Checkpoint> _checkpoints;
    private int _currentCheckPointIndex;
    
    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        _checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
        _currentCheckPointIndex = _checkpoints.Count > 0 ? 0 : -1;

#if UNITY_EDITOR
        if(DebugSpwan!=null)
            DebugSpwan.SpawnPlayer(Ship);
        else if (_currentCheckPointIndex != -1)
             _checkpoints[_currentCheckPointIndex].SpawnPlayer(Ship);
#else
         if (_currentCheckPointIndex != -1)
             _checkpoints[_currentCheckPointIndex].SpawnPlayer(Ship);
#endif

    }
    public void Update()
    {
        if (_checkpoints != null && Ship!=null) { 
            bool isAtLastCheckPoint = _currentCheckPointIndex + 1 >= _checkpoints.Count;
        if (isAtLastCheckPoint)
            return;
        float distanceToNextCheckpoint = _checkpoints[_currentCheckPointIndex + 1].transform.position.x - Ship.transform.position.x;
        if (distanceToNextCheckpoint >= 0)
            return;
        _checkpoints[_currentCheckPointIndex].PlayerLeftCheckpoint();
        _currentCheckPointIndex++;
        _checkpoints[_currentCheckPointIndex].PlayerHitCheckpoint();
    }
        // TODO BONUS
    }
    public void KillPlayer(bool withRotation)
    {
        StartCoroutine(KillPlayerCo(withRotation));
    }
    private IEnumerator KillPlayerCo(bool WithRotation)
    {
        Ship.GetComponent<ShipController>().Kill(WithRotation);
        CameraController.IsFollowing = false;
        yield return new WaitForSeconds(3f);
        CameraController.IsFollowing = true;
        if (_currentCheckPointIndex != -1)
            _checkpoints[_currentCheckPointIndex].SpawnPlayer(Ship);

        //TODO POINTS
    }

    public void EuropeanLevel()
    {
        SceneManager.LoadScene("Aigua");
    }
    public void AsianLevel()
    {
        SceneManager.LoadScene("Asian");
    }
    public void VikingLevel()
    {
        SceneManager.LoadScene("Viking");
    }
    public void Exit()
    {
        SceneManager.LoadScene("Level Selector");
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            SceneManager.LoadScene("Level Selector");
            }
    }



}
