using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    GameStateEventObj _GameStateObj;

    private void OnGUI()
    {
        if(GUILayout.Button("PLay"))
        {
            _GameStateObj.ChangeState.Invoke(GameStates.Start);
        }
    }

}
