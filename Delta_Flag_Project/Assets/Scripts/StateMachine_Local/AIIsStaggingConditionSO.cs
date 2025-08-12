using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AI_IsStagging", menuName = "SO/State Machines/Conditions/AI Is Stagging")]
public class AIIsStaggingConditionSO : StateConditionSO<AIIsStaggingCondition>
{
}

public class AIIsStaggingCondition : Condition
{
    private HealthBehaviour _health = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _health = _stateMachine.GetComponent<HealthBehaviour>();
    }

    protected override bool Statement()
    {
        return _health.IsStagging;
    }
}
