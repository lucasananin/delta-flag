using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Stagging", menuName = "SO/State Machines/Actions/AI Stagging")]
public class AIStaggingActionSO : StateActionSO<AIStaggingAction>
{
}

public class AIStaggingAction : StateAction
{
    private HealthBehaviour _health = null;
    private AIEntity _entity = null;
    private AIMover _mover = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _health = _stateMachine.GetComponent<HealthBehaviour>();
        _entity = _stateMachine.GetComponent<AIEntity>();
        _mover = _stateMachine.GetComponent<AIMover>();
    }

    public override void OnStateEnter()
    {
        _health.IsStagging = true;
        _timer = 0f;
        _mover.Stop();
        _entity.SetTargetEntity(_health.LastDamageModel.EntitySource);

        // play stag anim.
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 1f)
        {
            _health.IsStagging = false;
        }
    }
}
