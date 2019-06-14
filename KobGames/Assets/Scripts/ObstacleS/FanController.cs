using UnityEngine;

public class FanController : GenericObstacle
{
    [SerializeField]
    private GameLevelData _GameLevelData;

    [SerializeField]
    private GameObject _ExtraSpinner;

    [SerializeField]
    private CollisionHandler[] _ColHandler;

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private Transform _RotatingObject;

    private float _FlipVal;

    private float _SpeedRandomizer;

    private void Start()
    {
        bool ifDoubleSpinner = Random.Range(0, 3) == 2;
        _ExtraSpinner.SetActive(ifDoubleSpinner);

        _SpeedRandomizer = Random.Range(.75f, 1.2f);
        _FlipVal = 1 * _SpeedRandomizer;
        if (ifDoubleSpinner == true)
            _FlipVal *= .5f;

        foreach (var col in _ColHandler)
        {
            col.CollidedObj.AddListener(_ =>
            {
                Factory.Get<SFXManager>().PlaySFX(SFX.DieSpin);
                _.GetComponent<PlayerController>().KillOnSpot();
                if (_.tag == Constants.PLAYER_TAG)
                    _GameStateObj.ChangeState.Invoke(GameStates.Results);
            });
        }
    }

    private void Update()
    {
        _RotatingObject.Rotate(0, -_GameLevelData.SpinnerSpeed * _FlipVal, 0);
    }
}