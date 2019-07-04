using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private List<SFXCombination> _SFXList;

    [SerializeField]
    private AudioSource _BGMSFX, _LostBGMSFX, _WinSFX;

    private bool _MuteBGM;

    private void Awake()
    {
        Factory.Register<SFXManager>(this);
    }

    public void PlaySFX(SFX sfx)
    {
        if (sfx == SFX.Button && _MuteBGM)
        {
            _BGMSFX.mute = false;
            _MuteBGM = false;
        }
        if (sfx == SFX.Lose)
        {
            _MuteBGM = true;
            _BGMSFX.mute = true;
            _LostBGMSFX.Play();
        }
        if (sfx == SFX.Win)
        {
            _BGMSFX.mute = true;
            _WinSFX.Play();
        }

        var sf = _SFXList.Find(_ => _.SFX == sfx);
        sf.AudioSource.Play();
    }
}