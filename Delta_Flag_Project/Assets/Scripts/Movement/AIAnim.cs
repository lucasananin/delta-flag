using UnityEngine;

public class AIAnim : MonoBehaviour
{
    [SerializeField] Animator _anim = null;
    [SerializeField] AIMover _mover = null;

    private readonly int VELOCITY_HASH = Animator.StringToHash("Velocity");

    private void LateUpdate()
    {
        _anim.SetFloat(VELOCITY_HASH, _mover.GetVelocity().magnitude);
    }
}
