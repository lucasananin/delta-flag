using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Alert", menuName = "SO/State Machines/Actions/AI Alert")]
public class AIAlertActionSO : StateActionSO<AIAlertAction>
{
}

public class AIAlertAction : StateAction
{
    // if target is visible, stop movement, rotate to target and start shooting.
    // if target is not visible, search for a position where he is visible and rotate to movement.

    private AIMover _mover = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _mover = _stateMachine.GetComponent<AIMover>();
    }

    public override void OnStateEnter()
    {
        _mover.Stop();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}