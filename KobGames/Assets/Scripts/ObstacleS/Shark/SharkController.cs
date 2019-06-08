using UnityEngine;

public class SharkController : GenericObstacle
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
            if (_.tag == Constants.PLAYER_TAG)
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
        });

        _SharkModel.CallWarningVFX.AddListener(() =>
        {
            Vector3 newSharkPos = new Vector3(_SharkView.MovableTransform.position.x, -3, _SharkView.MovableTransform.position.z);
            _SharkView.UpdatePosition(newSharkPos);
            Vector3 newRot = new Vector3(0, _SharkModel.StartAtLeft ? 90 : -90, 0);
            _SharkView.UpdateRotation(newRot);

            _SharkViewAnimator.SetReady();
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
            _SharkView.UpdatePosition(_);
        });
        _SharkModel.TriggerObstacle.AddListener(() =>
        {
            _SharkView.TriggerTrap(_SharkModel.GetPathList(), _SharkModel.StartAtLeft);
            _SharkViewAnimator.SetAttack();
        });
    }
}