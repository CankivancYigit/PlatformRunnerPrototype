using UnityEngine;
using TMPro;

public class DisplayName : MonoBehaviour
{
    public GameObject targetObject;
    private TextMeshPro _textMeshPro;

    void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = targetObject.name;
    }
}

