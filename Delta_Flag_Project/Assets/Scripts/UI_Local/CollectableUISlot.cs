using TMPro;
using UnityEngine;

public class CollectableUISlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    public void Init(string _message)
    {
        _text.text = $"{_message}";
        Invoke(nameof(Disable), 5f);
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
}
