using UnityEngine;

public class AmmoCollectable : CollectableBehaviour
{
    [SerializeField] AmmoSO _type = null;
    [SerializeField] Vector2Int _percentageRange = new(8, 12);

    public override void Collect(CollectableAgent _agent)
    {
        if (_agent.TryGetComponent(out AmmoHandler _ammo))
        {
            if (_ammo.IsFull(_type)) return;

            var _amount = Random.Range(_percentageRange.x, _percentageRange.y);
            _ammo.RestoreAmmo(_type, _amount);
            base.Collect(_agent);
        }
    }
}
