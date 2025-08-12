using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Stagger", menuName = "SO/State Machines/Actions/AI Stagger")]
public class AIStaggerActionSO : StateActionSO<AIStaggerAction>
{
}

public class AIStaggerAction : StateAction
{
    private HealthBehaviour _health = null;
    private AIEntity _entity = null;
    private AIMover _mover = null;
    private AIAnim _anim = null;
    private float _timer = 0f;

    public override void Awake(StateMachine _stateMachine)
    {
        _health = _stateMachine.GetComponent<HealthBehaviour>();
        _entity = _stateMachine.GetComponent<AIEntity>();
        _mover = _stateMachine.GetComponent<AIMover>();
        _anim = _stateMachine.GetComponent<AIAnim>();
    }

    public override void OnStateEnter()
    {
        _health.IsStaggering = true;
        _timer = 0f;
        _mover.Stop();
        _entity.SetTargetEntity(_health.LastDamageModel.EntitySource);
        _anim.SetIsAlert(true);
        _anim.TriggerStag();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        if (_health.WasDamagedThisFrame)
        {
            _anim.TriggerStag();
            _timer = 0;
        }

        _timer += Time.deltaTime;

        float _staggeringTime = 0.5f;
        if (_timer > _staggeringTime)
        {
            _health.IsStaggering = false;
        }
    }
}
