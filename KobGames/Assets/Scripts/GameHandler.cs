using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
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
    private GameLevelData _GameData;
    private void Start()
    {

        _GameStateObj.ChangeState.AddListener(_ => 
        {
            if(_ == GameStates.Results)
            {
                GameObject tempTrans = new GameObject();
                tempTrans.transform.position = _Players[0].transform.position;
                _Vcam.Follow = tempTrans.transform;
                _Vcam.LookAt = tempTrans.transform;
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

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(.5f);
        StartGame();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        _Players[0].PlayerModel.InjectNodes(_PlatformManager.GetPathList(1));
        _Players[1].PlayerModel.InjectNodes(_PlatformManager.GetPathList(2));
        _Players[2].PlayerModel.InjectNodes(_PlatformManager.GetPathList(3));
        _GameStateObj.ChangeState.Invoke(GameStates.Start);
    }

}
