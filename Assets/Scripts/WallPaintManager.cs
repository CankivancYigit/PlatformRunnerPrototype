using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallPaintManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject wallObject;
    public Color paintColor = Color.red;
    public Color color1 = Color.red;
    public Color color2 = Color.green;
    public Color color3 = Color.blue;
    public float brushSize = 0.05f;
    public float maxBrushSize = 0.1f;

    public Button colorButton1;
    public Button colorButton2;
    public Button colorButton3;
    public Slider brushSizeSlider;
    public TextMeshProUGUI percentageText;

    private Texture2D texture;
    private Renderer wallRenderer;
    private float totalPixels;
    private float paintedPixels;
    private bool _canPaint = true;
    void Start()
    {
        if (wallObject != null)
        {
            wallRenderer = wallObject.GetComponent<Renderer>();

            if (wallRenderer.material.mainTexture == null)
            {
                texture = new Texture2D(1024, 1024);
                wallRenderer.material.mainTexture = texture;
            }
            else
            {
                texture = (Texture2D)wallRenderer.material.mainTexture;
            }

            totalPixels = texture.width * texture.height;
        }

        colorButton1.onClick.AddListener(() => SetPaintColor(color1));
        colorButton2.onClick.AddListener(() => SetPaintColor(color2));
        colorButton3.onClick.AddListener(() => SetPaintColor(color3));

        brushSizeSlider.value = brushSize;
        brushSizeSlider.maxValue = maxBrushSize;
        brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
    }
    
    void Update()
    {
        if (_canPaint)
        {
            if (Input.GetMouseButton(0) && wallObject != null)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == wallObject)
                {
                    Vector2 uv = hit.textureCoord;
                    PaintOnWall(uv);
                }
            }
        }
        //UpdatePercentageText();
    }

    void PaintOnWall(Vector2 uv)
    {
        int x = (int)(uv.x * texture.width);
        int y = (int)(uv.y * texture.height);
        int brushPixelSize = Mathf.FloorToInt(brushSize * texture.width);
        bool isPainted = false;

        for (int i = -brushPixelSize; i < brushPixelSize; i++)
        {
            for (int j = -brushPixelSize; j < brushPixelSize; j++)
            {
                if (x + i >= 0 && x + i < texture.width && y + j >= 0 && y + j < texture.height)
                {
                    Color currentColor = texture.GetPixel(x + i, y + j);
                
                    // Eğer mevcut piksel boyanmamışsa
                    if (currentColor != paintColor && currentColor != color1 && currentColor != color2 && currentColor != color3)
                    {
                        texture.SetPixel(x + i, y + j, paintColor);
                        isPainted = true;
                    }
                }
            }
        }

        if (isPainted)
        {
            paintedPixels += Mathf.Pow(brushPixelSize * 2, 2);
            UpdatePercentageText();
        }

        texture.Apply();
    }

    
    void UpdatePercentageText()
    {
        if (percentageText != null)
        {
            float percentage = (paintedPixels / totalPixels / 6f) * 100f;
            percentageText.text = $"{Mathf.Clamp(percentage, 0f, 100f):F2}%";

            if (percentage >= 100)
            {
                EventBus<WallPaintFinishEvent>.Emit(this,new WallPaintFinishEvent());
                _canPaint = false;
            }
        }
    }

    void SetPaintColor(Color color)
    {
        paintColor = color;
    }

    void SetBrushSize(float size)
    {
        brushSize = size;
    }
}
