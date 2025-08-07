using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AI_HasTargetEntity", menuName = "SO/State Machines/Conditions/AI Has Target Entity")]
public class AIHasTargetEntityConditionSO : StateConditionSO<AIHasTargetEntityCondition>
{
}

public class AIHasTargetEntityCondition : Condition
{
    private AiEntity _aiEntity = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AiEntity>();
    }

    protected override bool Statement()
    {
        return _aiEntity.HasTargetEntity();
    }
}