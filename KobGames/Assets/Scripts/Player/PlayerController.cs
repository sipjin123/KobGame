using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericController
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    void Start()
    {
        _GameStateObj.ChangeState.AddListener(_ =>
        {
            switch(_)
            {
                case GameStates.Start:
                    _PlayerModel.UpdateState(CharacterStates.Running);
                    break;
                case GameStates.Results:

                    break;
            }
        });
    }
}
