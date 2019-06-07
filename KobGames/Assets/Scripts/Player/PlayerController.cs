using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericController
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    public void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            _PlayerView._RunEvent.Invoke(true);
            _PlayerViewAnim.SetTargetSpeed(1);
        }
        else
        {
            _PlayerView._RunEvent.Invoke(false);
            _PlayerViewAnim.SetTargetSpeed(0);
        }
    }

    void Start()
    {
        _PlayerView._TargetReachedEvent.AddListener(() =>
        {
            var temp = _PlayerModel.GetNextNode();
            if(temp == null)
            {
                Debug.LogError("End of GAme");
                return;
            }
            _PlayerView._TargetPosEvent.Invoke(temp.position);
        });

        _GameStateObj.ChangeState.AddListener(_ =>
        {
            switch(_)
            {
                case GameStates.Start:
                    _PlayerView._SpeedEvent.Invoke(_PlayerModel.GetSpeed());
                    _PlayerView._TargetPosEvent.Invoke(_PlayerModel.CurrentNode.position);
                    _PlayerModel.UpdateState(CharacterStates.Idle);
                    break;
                case GameStates.Results:

                    break;
            }
        });
    }
}
