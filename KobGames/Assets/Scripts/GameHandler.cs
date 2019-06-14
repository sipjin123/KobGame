using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _LevelDataList;

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private PlatformController _FirstPlatform;

    [SerializeField]
    private List<PlayerController> _Players;

    [SerializeField]
    private PlatformManager _PlatformManager;

    [SerializeField]
    private CinemachineVirtualCamera _Vcam;

    [SerializeField]
    private ObstacleSpawner _ObstacleSpawner;

    [SerializeField]
    private GameLevelData _GameData;

    [SerializeField]
    private UIHandler _UIHandler;

    [SerializeField]
    private float _Timer;

    [SerializeField]
    private bool _TimerStart;

    [SerializeField]
    private LevelData _CurrentLevelData;

    int randomPlatform = 0;
    private void Awake()
    {
        _PlatformManager.SpawnEvent.AddListener(_ =>
        {
            _ObstacleSpawner.RequestObstacleAt(_);
        });
    }

    private void Start()
    {
        randomPlatform = Random.Range(0, _LevelDataList.Count);
        _CurrentLevelData = _LevelDataList[randomPlatform];

        _UIHandler.SetHighScore(PlayerPrefs.GetFloat(Constants.Pref_HighScore));
        _PlatformManager.InjectPlatformRequests(_CurrentLevelData.PlatformType);
        _FirstPlatform = _PlatformManager.GetFirstPlatform();

        _GameStateObj.ChangeState.AddListener(_ =>
        {
            if (_ == GameStates.Results)
            {
                GameObject tempTrans = new GameObject();
                tempTrans.transform.position = _Players[0].transform.position;
                _Vcam.Follow = tempTrans.transform;
                _Vcam.LookAt = tempTrans.transform;
            }
            else if(_ == GameStates.Win)
            {
                var highSCore = PlayerPrefs.GetFloat(Constants.Pref_HighScore);
                Debug.LogError("My score is : " + highSCore);
                if (_Timer < highSCore || highSCore < 1)
                {
                    PlayerPrefs.SetFloat(Constants.Pref_HighScore, _Timer);
                    _UIHandler.SetHighScore(_Timer);
                }
            }
        });

        if (_GameData.HasLaunched == true)
        {
            StartCoroutine(DelayStart());
        }
    }

    private void OnApplicationQuit()
    {
        _GameData.HasLaunched = false;
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(.5f);
        StartGame();
    }

    private void Update()
    {
        if(_TimerStart)
        {
            _Timer += Time.deltaTime;
            _UIHandler.SetTime(_Timer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        _TimerStart = true;
        Factory.Get<SFXManager>().PlaySFX(SFX.Button);
        _Players[0].PlayerModel.InjectNodes(_PlatformManager.GetPathList(1));
        _Players[1].PlayerModel.InjectNodes(_PlatformManager.GetPathList(2));
        _Players[2].PlayerModel.InjectNodes(_PlatformManager.GetPathList(3));
        _GameStateObj.ChangeState.Invoke(GameStates.Start);
    }
}