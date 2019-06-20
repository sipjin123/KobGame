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
    private Canvas _EnemyWinCanvas;

    [SerializeField]
    private Canvas _WinCanvas;

    [SerializeField]
    private Animator _ResultAnim, _LoseAnim;

    [SerializeField]
    private Button _RetryButton, _RetryLoseButton,_MenuLoseButton, _RetryWinButton, _QuitButton , _MenuButton;

    [SerializeField]
    private GameLevelData _GameData;

    [SerializeField]
    private Text  _HighSCore, _Time;

    [SerializeField]
    private GameObject _HintObj;

    bool _GameHasEnded;

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
                    _HintObj.SetActive(true);   
                    break;

                case GameStates.Win:
                    if (_GameHasEnded)
                        return;
                    _GameHasEnded = true;
                    _WinCanvas.enabled = true;
                    Factory.Get<SFXManager>().PlaySFX(SFX.Win);
                    break;
                    
                case GameStates.Results:
                    if (_GameHasEnded)
                        return;
                    _GameHasEnded = true;
                    StartCoroutine(DelayGameOver());
                    break;
                case GameStates.Lose:
                    if (_GameHasEnded)
                        return;
                    _GameHasEnded = true;
                    StartCoroutine(DelayLose());
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

        _MenuLoseButton.onClick.AddListener(() =>
        {
            _GameData.HasLaunched = false;
            ResetGame();
        });
        _RetryLoseButton.onClick.AddListener(() => ResetGame());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ResetGame();
        }
    }

    public void SetTime(float time)
    {
        int minutes = (int)time / 60;
        float seconds = time % 60;

        _Time.text = minutes+" : " + seconds.ToString("f1");
    }
    
    public void SetHighScore(float score)
    {
        _HighSCore.text = "HighScore: " + score;
    }
    private IEnumerator DelayLose()
    {
        yield return new WaitForSeconds(1.5f);
        _EnemyWinCanvas.enabled = true;
        Factory.Get<SFXManager>().PlaySFX(SFX.Lose);
        _LoseAnim.Play(AnimConstants.ANIM_START);
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