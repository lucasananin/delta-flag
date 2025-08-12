using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AI_HasTakenDamage", menuName = "SO/State Machines/Conditions/AI Has Taken Damage")]
public class AIHasTakenDamageConditionSO : StateConditionSO<AIHasTakenDamageCondition>
{
}

public class AIHasTakenDamageCondition : Condition
{
    private HealthBehaviour _health = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _health = _stateMachine.GetComponent<HealthBehaviour>();
    }

    protected override bool Statement()
    {
        return _health.WasDamagedThisFrame;
    }
}
