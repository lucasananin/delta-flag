using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AI_HasTargetEntity", menuName = "SO/State Machines/Conditions/AI Has Target Entity")]
public class AIHasTargetEntityConditionSO : StateConditionSO<AIHasTargetEntityCondition>
{
}

public class AIHasTargetEntityCondition : Condition
{
    private EntityDetector _detector = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _detector = _stateMachine.GetComponentInChildren<EntityDetector>();
    }

    protected override bool Statement()
    {
        return _detector.HasTargetWithin();
    }
}