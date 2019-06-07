using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private PlatformModel _PlatformModel;
    public PlatformModel PlatformModel { get { return _PlatformModel; } }
    [SerializeField]
    private PlatformView _PlatformView;
    public PlatformView PlatformView { get { return _PlatformView; } }

}
