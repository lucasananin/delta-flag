using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Condition_AI_WasDamaged", menuName = "SO/State Machines/Conditions/AI Was Damaged")]
public class AIWasDamagedConditionSO : StateConditionSO<AIWasDamagedCondition>
{
}

public class AIWasDamagedCondition : Condition
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
