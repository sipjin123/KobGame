using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    [SerializeField]
    private SharkModel _SharkModel;
    [SerializeField]
    private SharkView _SharkView;
    [SerializeField]
    private SharkViewAnimator _SharkViewAnimator;

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private CollisionHandler _CollisionHandler;

    private void Start()
    {
        _CollisionHandler.CollidedObj.AddListener(_ => 
        {
            _.GetComponent<PlayerController>().AddParent(_SharkModel.MouthLocation.transform);
            if(_.tag == Constants.PLAYER_TAG)
            _GameStateObj.ChangeState.Invoke(GameStates.Results);
        });

        _SharkModel.CallWarningVFX.AddListener(() => 
        {
            _SharkView.InitWarningVFX();
        });

        _SharkView.TargetReachedEvent.AddListener(() =>
        {
            var temp = _SharkModel.GetNextNode();
            if (temp == Vector3.zero)
            {
                _SharkViewAnimator.SetIdle();
                _SharkView.EndOfPath();
                _SharkModel.Return();
                return;
            }
            _SharkView.TargetPosEvent.Invoke(temp);
        });
        _SharkModel.StartPostUpdate.AddListener(_ => 
        {
            _SharkView.UpdatePosition( _);
        });
        _SharkModel.TriggerObstacle.AddListener(() => 
        {
            _SharkView.TriggerTrap(_SharkModel.GetPathList(),_SharkModel.StartAtLeft);
            _SharkViewAnimator.SetAttack();

        });
    }

}
