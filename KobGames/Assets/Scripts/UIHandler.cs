using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    private Button _RetryButton, _RetryWinButton, _QuitButton;

    [SerializeField]
    private GameLevelData _GameData;
    void Start()
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
                    break;
                case GameStates.Results:
                    StartCoroutine(DelayGameOver());
                    break;
            }
        });

        _RetryButton.onClick.AddListener(() => ResetGame());
        _RetryWinButton.onClick.AddListener(() => ResetGame());
        _QuitButton.onClick.AddListener(() => QuitGame());
    }
    IEnumerator DelayGameOver()
    {

        yield return new WaitForSeconds(1.5f);
        _ResultsCanvas.enabled = true;
        _ResultAnim.Play(AnimConstants.ANIM_START);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(Constants.Level_Name);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
