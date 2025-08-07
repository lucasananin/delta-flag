using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Patrol", menuName = "SO/State Machines/Actions/AI Patrol")]
public class AIPatrolActionSO : StateActionSO<AIPatrolAction>
{
}

public class AIPatrolAction : StateAction
{
    private AIEntity _aiEntity = null;
    private AIPatrol _aiPatrol = null;
    private AIMover _mover = null;
    private EntityDetector _detector = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiPatrol = _stateMachine.GetComponent<AIPatrol>();
        _aiEntity = _stateMachine.GetComponent<AIEntity>();
        _mover = _stateMachine.GetComponent<AIMover>();
        _detector = _stateMachine.GetComponentInChildren<EntityDetector>();
    }

    public override void OnStateEnter()
    {
        _aiPatrol.StartPatrol();
    }

    public override void OnStateExit()
    {
        _aiPatrol.EndPatrol();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        if (!_aiEntity.HasTargetEntity() && _detector.HasTargetWithin(out GameObject _targetFound))
        {
            var _entity = _targetFound.GetComponent<EntityBehaviour>();
            _aiEntity.SetTargetEntity(_entity);
        }

        _mover.RotateToMovement();
    }
}