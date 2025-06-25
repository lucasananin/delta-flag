using System.Collections.Generic;
using UnityEngine;

public class GeneralMethods : MonoBehaviour
{
    public static bool HasAvailableTag<T>(GameObject _gameObjectHit, IReadOnlyList<T> _tags)
    {
        int _count = _tags.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_gameObjectHit.CompareTag(_tags[i] as string))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsTheSameString(string _a, string _b)
    {
        return string.Equals(_a, _b, System.StringComparison.OrdinalIgnoreCase);
    }
}
