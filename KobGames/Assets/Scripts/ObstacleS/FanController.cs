using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController :  GenericObstacle
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
     
    private void Start()
    {
        bool ifDoubleSpinner = Random.Range(0, 3) == 2;
        _ExtraSpinner.SetActive(ifDoubleSpinner);

        if (ifDoubleSpinner == false)
            _FlipVal = 1;
        else
            _FlipVal = .5f;


        foreach (var col in _ColHandler)
        {
            col.CollidedObj.AddListener(_ =>
            {
                _.GetComponent<PlayerController>().KillOnSpot();
                if(_.tag == Constants.PLAYER_TAG)
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
            });
        }
    }

    private void Update()
    {
        _RotatingObject.Rotate(0, -_GameLevelData.SpinnerSpeed * _FlipVal, 0);
    }
}
