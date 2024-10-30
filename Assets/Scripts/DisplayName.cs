using UnityEngine;
using TMPro;

public class DisplayName : MonoBehaviour
{
    private TextMeshPro _textMeshPro;

    void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = transform.parent.gameObject.name;
    }
}

