using UnityEngine;
using UnityEngine.Events;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent<CollectableAgent> _onCollected = null;

    public event UnityAction<CollectableAgent> OnCollected = null;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out CollectableAgent _agent))
        {
            Collect(_agent);
        }
    }

    public virtual void Collect(CollectableAgent _agent)
    {
        OnCollected?.Invoke(_agent);
        _onCollected?.Invoke(_agent);
        Destroy(gameObject);
    }
}
