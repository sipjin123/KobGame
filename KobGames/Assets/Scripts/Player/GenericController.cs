using UnityEngine;

public class GenericController : MonoBehaviour
{
    [Header("Components")]
    [Space(10)]
    [SerializeField]
    protected PlayerView _PlayerView;

    [SerializeField]
    protected PlayerViewAnimation _PlayerViewAnim;

    [SerializeField]
    protected PlayerModel _PlayerModel;

    public PlayerModel PlayerModel { get { return _PlayerModel; } }

    [SerializeField]
    protected Collider _Collider;

    public Collider Collider { get { return _Collider; } }
}