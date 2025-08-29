using UnityEngine;

public class HealthCollectable : CollectableBehaviour
{
    [SerializeField] int _restorePercentage = 10;

    public override void Collect(CollectableAgent _agent)
    {
        if (_agent.TryGetComponent(out HealthBehaviour _health))
        {
            // if health is full, return.
            _health.RestoreHealth(_restorePercentage);
            base.Collect(_agent);
        }
    }
}
