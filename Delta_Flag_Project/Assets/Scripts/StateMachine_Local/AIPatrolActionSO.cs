using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Patrol", menuName = "SO/State Machines/Actions/AI Patrol")]
public class AIPatrolActionSO : StateActionSO<AIPatrolAction>
{
}

public class AIPatrolAction : StateAction
{
    private AIPatrol _aiPatrol = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiPatrol = _stateMachine.GetComponent<AIPatrol>();
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
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
}