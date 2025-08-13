using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Dead", menuName = "SO/State Machines/Actions/AI Dead")]
public class AIDeadActionSO : StateActionSO<AIDeadAction>
{
}

public class AIDeadAction : StateAction
{
    private AIAnim _anim = null;
    private Collider _collider = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _anim = _stateMachine.GetComponent<AIAnim>();
        _collider = _stateMachine.GetComponent<Collider>();
    }

    public override void OnStateEnter()
    {
        _collider.enabled = false;
        _anim.TriggerDead();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}
