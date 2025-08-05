using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] AIMover _aiMover = null;
    [SerializeField] Vector2 _waitRange = new(2f, 3f);
    [SerializeField] List<Transform> _points = null;

    [Header("// READONLY")]
    [SerializeField] int _index = 0;

    private void Start()
    {
        transform.position = _points[0].position;
        StartPatrol();
    }

    public void StartPatrol()
    {
        StartCoroutine(Patrol_Routine());
    }

    public void EndPatrol()
    {
        StopAllCoroutines();
    }

    public IEnumerator Patrol_Routine()
    {
        while (true)
        {
            var _point = _points[_index];
            _aiMover.SetDestination(_point.position);
            yield return null;

            while (!_aiMover.HasReachedDestination())
            {
                yield return null;
            }

            var _waitTime = Random.Range(_waitRange.x, _waitRange.y);
            yield return new WaitForSeconds(_waitTime);

            _index++;

            if (_index >= _points.Count)
            {
                _index = 0;
            }
        }
    }
}
