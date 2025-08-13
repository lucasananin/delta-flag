using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Dead", menuName = "SO/State Machines/Actions/AI Dead")]
public class AIDeadActionSO : StateActionSO<AIDeadAction>
{
}

public class AIDeadAction : StateAction
{
    // disable collider.
    // play animation.

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}
