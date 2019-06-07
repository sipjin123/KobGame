using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private PlatformController _FirstPlatform; 
    private void OnGUI()
    {
        if(GUILayout.Button("PLay"))
        {
            _GameStateObj.ChangeState.Invoke(GameStates.Start);
        }
    }

}
