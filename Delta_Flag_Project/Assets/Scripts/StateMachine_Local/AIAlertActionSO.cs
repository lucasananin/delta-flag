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

    private AiEntity _aiEntity = null;
    private AIMover _mover = null;
    private AIAnim _anim = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
        _mover = _stateMachine.GetComponent<AIMover>();
        _anim = _stateMachine.GetComponent<AIAnim>();
    }

    public override void OnStateEnter()
    {
        _mover.Stop();
        _anim.SetIsAlert(true);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        var _targetDirection = (_aiEntity.GetTargetEntityPosition() - _mover.transform.position).normalized;
        _targetDirection.y = 0;
        _mover.RotateTo(_targetDirection);
    }
}