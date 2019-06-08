﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private Canvas _TitleCanvas;

    [SerializeField]
    private Canvas _ResultsCanvas;

    [SerializeField]
    private Canvas _WinCanvas;

    [SerializeField]
    private Animator _ResultAnim;

    [SerializeField]
    private Button _RetryButton, _RetryWinButton, _QuitButton , _MenuButton;

    [SerializeField]
    private GameLevelData _GameData;

    private void Start()
    {
        if (_GameData.HasLaunched == false)
        {
            _TitleCanvas.enabled = true;
            _GameData.HasLaunched = true;
        }

        _GameStateObj.ChangeState.AddListener(_ =>
        {
            switch (_)
            {
                case GameStates.Start:
                    _TitleCanvas.enabled = false;
                    break;

                case GameStates.Win:
                    _WinCanvas.enabled = true;
                    Factory.Get<SFXManager>().PlaySFX(SFX.Win);
                    break;
                    
                case GameStates.Results:
                    StartCoroutine(DelayGameOver());
                    break;
            }
        });

        _RetryButton.onClick.AddListener(() => ResetGame());
        _RetryWinButton.onClick.AddListener(() => ResetGame());
        _MenuButton.onClick.AddListener(() => 
        {
            _GameData.HasLaunched = false;
            ResetGame();
        });
        _QuitButton.onClick.AddListener(() => QuitGame());
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        _ResultsCanvas.enabled = true;
        Factory.Get<SFXManager>().PlaySFX(SFX.Lose);
        _ResultAnim.Play(AnimConstants.ANIM_START);
    }

    private void ResetGame()
    {
        Factory.Get<SFXManager>().PlaySFX(SFX.Button);
        SceneManager.LoadScene(Constants.Level_Name);
    }

    private void QuitGame()
    {
        Factory.Get<SFXManager>().PlaySFX(SFX.Button);
        Application.Quit();
    }
}