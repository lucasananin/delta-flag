using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_Ai_IsCloseToTarget", menuName = "SO/State Machines/Conditions/AI Is Is Close To Target")]
public class AiIsCloseToTargetConditionSO : StateConditionSO<AiIsCloseToTargetCondition>
{
}

public class AiIsCloseToTargetCondition : Condition
{
    private AIEntity _aiEntity = null;
    private AIEntitySO _aiEntitySO = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AIEntity>();
        _aiEntitySO = _aiEntity.GetEntitySO<AIEntitySO>();
    }

    protected override bool Statement()
    {
        return _aiEntity.IsCloseToTargetEntity(_aiEntitySO.MinTargetDistance);
    }
}