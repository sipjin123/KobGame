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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGame();
        }
    }

    private void OnGUI()
    {
        if(GUILayout.Button( "PLay", GUILayout.Height(100),GUILayout.Width(100)))
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
