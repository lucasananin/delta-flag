using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Action_AI_Alert", menuName = "SO/State Machines/Actions/AI Alert")]
public class AIAlertActionSO : StateActionSO<AIAlertAction>
{
}

public class AIAlertAction : StateAction
{
    private AIEntity _aiEntity = null;
    private AIMover _mover = null;
    private AIAnim _anim = null;
    private AIWeaponHandler _weaponHandler = null;

    public override void Awake(StateMachine _stateMachine)
    {
        _aiEntity = _stateMachine.GetComponent<AIEntity>();
        _mover = _stateMachine.GetComponent<AIMover>();
        _anim = _stateMachine.GetComponent<AIAnim>();
        _weaponHandler = _stateMachine.GetComponent<AIWeaponHandler>();
    }

    public override void OnStateEnter()
    {
        var _alertMoveSpeed = 5f;
        _mover.SetSpeed(_alertMoveSpeed);
        _anim.SetIsAlert(true);
        SetShootTriggerAnim(true);
    }

    public override void OnStateExit()
    {
        SetShootTriggerAnim(false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        var _maxTargetDistance = 10f;
        var _isCloseToTarget = _aiEntity.IsCloseToTargetEntity(_maxTargetDistance);

        if (_aiEntity.IsTargetOnLineOfSight && _isCloseToTarget)
        {
            _mover.Stop();
            var _targetDirection = (_aiEntity.GetTargetEntityPosition() - _mover.transform.position).normalized;
            _targetDirection.y = 0;
            _mover.RotateTo(_targetDirection);
            _weaponHandler.TryShootAll(_aiEntity);
        }
        else
        {
            _weaponHandler.StopShooting();
            var _positionNearTarget = TryGetPositionWhereTargetIsVisible();
            _mover.SetDestination(_positionNearTarget);
            _mover.RotateToMovement();
        }
    }

    private Vector3 TryGetPositionWhereTargetIsVisible()
    {
        var _moveRange = new Vector2(1, 2);
        var _numberOfTries = 10;
        var _positionNearTarget = _aiEntity.PickRandomPointNearTarget(_moveRange);

        for (int i = 0; i < _numberOfTries; i++)
        {
            if (_aiEntity.CanSeeTargetFromPoint(_positionNearTarget))
            {
                return _positionNearTarget;
            }
            else
            {
                _positionNearTarget = _aiEntity.PickRandomPointNearTarget(_moveRange);
            }
        }

        return _positionNearTarget;
    }

    private void SetShootTriggerAnim(bool _subscribe)
    {
        var _weapons = _weaponHandler.GetAllWeapons();
        int _count = _weapons.Count;
        for (int i = 0; i < _count; i++)
        {
            if (_subscribe)
                _anim.SubscribeShoot(_weapons[i]);
            else
                _anim.UnsubscribeShoot(_weapons[i]);
        }
    }
}