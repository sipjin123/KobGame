﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private List<SFXCombination> _SFXList;

    [SerializeField]
    private AudioSource _BGMSFX, _LostBGMSFX;

    bool _MuteBGM;

    private void Awake()
    {
        Factory.Register<SFXManager>(this);
    }

    public void PlaySFX(SFX sfx)
    {
        if(sfx == SFX.Button && _MuteBGM)
        {
            _BGMSFX.mute = false;
            _MuteBGM = false;
        }
        if(sfx == SFX.Lose)
        {
            _MuteBGM = true;
            _BGMSFX.mute = true;
            _LostBGMSFX.Play();
        }

        var sf = _SFXList.Find(_ => _.SFX == sfx);
        sf.AudioSource.Play();
    }
}
[Serializable]
public class SFXCombination
{
    public SFX SFX;
    public AudioSource AudioSource;
}