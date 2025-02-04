using Animancer;
using UnityEngine;

public class Animancer_Test_Script : MonoBehaviour
{

    [SerializeField] private AnimancerComponent _Animancer;

    public ClipTransition _mech_walk_tranistion;
    public ClipTransition _mech_idle_transition;
    public ClipTransition _mech_shoot_transition;

    public AvatarMask _ActionMask;

    private AnimancerLayer _BaseLayer;
    private AnimancerLayer _ActionLayer;

    private float _ActionFadeOutDuration = AnimancerGraph.DefaultFadeDuration;

    public enum mech_State
    {
        NotActing,
        Acting,
    }
    public mech_State _CurrentState;

    protected virtual void Awake()
    {
        _BaseLayer = _Animancer.Layers[0];
        _ActionLayer = _Animancer.Layers[1];
        //_Action.Events.OnEnd = OnEnable;
        _ActionLayer.SetDebugName("Gun Layer");
        _Animancer.Layers.SetMask(1, _ActionMask);

        _mech_shoot_transition.Events.OnEnd = OnActionEnd;

    }

    protected virtual void OnEnable()
    {
        _Animancer.Play(_mech_idle_transition);
    }

    protected virtual void Update()
    {
        //switch (_CurrentState)
        //{
        //    case mech_State.NotActing:
        //        UpdateMovement();
        //        UpdateAction();
        //        break;

        //    case mech_State.Acting:
        //        UpdateAction();
        //        break;
        //}

        UpdateMovement();
        UpdateAction();
    }

    public void UpdateAction()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _ActionLayer.Play(_mech_shoot_transition);
        }
    }

    public void UpdateMovement()
    {
        _CurrentState = mech_State.NotActing;

        if (Input.GetKey(KeyCode.W))
        {
            _CurrentState = mech_State.Acting;
            _BaseLayer.Play(_mech_walk_tranistion);
        }

        else
        {

            _BaseLayer.Play(_mech_idle_transition);
        }
     

    }

    public void OnActionEnd()
    {
        _ActionLayer.StartFade(0, _ActionFadeOutDuration);
    }
}


