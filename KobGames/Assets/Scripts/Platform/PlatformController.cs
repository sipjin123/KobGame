using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private PlatformModel _PlatformModel;

    public PlatformModel PlatformModel { get { return _PlatformModel; } }

    [SerializeField]
    private PlatformView _PlatformView;

    public PlatformView PlatformView { get { return _PlatformView; } }

    [SerializeField]
    private PlatformType _PlatformType;

    public PlatformType PlatformType { get { return _PlatformType; } }
}