using UnityEngine;
using TMPro;

public class DisplayName : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    private Transform _mainCameraTransform;
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = transform.parent.gameObject.name;
        _mainCameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        transform.rotation = _mainCameraTransform.rotation;
    }
}

