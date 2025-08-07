using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AIMoveToTarget", menuName = "SO/State Machines/Actions/AI Move To Target")]
public class MoveToTargetActionSO : StateActionSO<MoveToTargetAction>
{
}

public class MoveToTargetAction : StateAction
{
    private AIEntity _aiEntity = null;
    private AIEntitySO _aiEntitySO = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AIEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AIEntitySO>();
    }

    public override void OnFixedUpdate()
    {
        //
    }

    public override void OnUpdate()
    {
        if (_aiEntity.IsCloseToTargetEntity(_aiEntitySO.MoveToTargetDistance))
        {
            SetPath();
        }
    }

    private void SetPath()
    {
        var _point = _aiEntity.GetTargetEntityPosition();
        _aiEntity.SetAIPathDestination(_point);
    }
}