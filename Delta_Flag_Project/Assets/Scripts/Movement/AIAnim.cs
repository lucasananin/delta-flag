using UnityEngine;

public class AIAnim : MonoBehaviour
{
    [SerializeField] Animator _anim = null;
    [SerializeField] AIMover _mover = null;

    private readonly int VELOCITY_HASH = Animator.StringToHash("Velocity");
    private readonly int IS_ALERT_HASH = Animator.StringToHash("IsAlert");

    private void LateUpdate()
    {
        _anim.SetFloat(VELOCITY_HASH, _mover.GetVelocity().magnitude);
    }

    public void SetIsAlert(bool _value)
    {
        _anim.SetBool(IS_ALERT_HASH, _value);
    }
}
