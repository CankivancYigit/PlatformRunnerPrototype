using UnityEngine;
using UnityEngine.UI;

public class WallPaintManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject wallObject;
    public Color paintColor = Color.red;
    public float brushSize = 0.05f;
    public float maxBrushSize = 0.1f;

    public Button colorButton1;
    public Button colorButton2;
    public Button colorButton3;
    public Slider brushSizeSlider;

    private Texture2D texture;
    private Renderer wallRenderer;

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
        }
        
        colorButton1.onClick.AddListener(() => SetPaintColor(Color.yellow));
        colorButton2.onClick.AddListener(() => SetPaintColor(Color.red));
        colorButton3.onClick.AddListener(() => SetPaintColor(Color.blue));
        
        brushSizeSlider.value = brushSize;
        brushSizeSlider.maxValue = maxBrushSize;
        brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
    }

    void Update()
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

    void PaintOnWall(Vector2 uv)
    {
        int x = (int)(uv.x * texture.width);
        int y = (int)(uv.y * texture.height);
        
        int brushPixelSize = Mathf.FloorToInt(brushSize * texture.width);

        // Boyama döngüsü
        for (int i = -brushPixelSize; i < brushPixelSize; i++)
        {
            for (int j = -brushPixelSize; j < brushPixelSize; j++)
            {
                if (x + i >= 0 && x + i < texture.width && y + j >= 0 && y + j < texture.height)
                {
                    texture.SetPixel(x + i, y + j, paintColor);
                }
            }
        }

        texture.Apply();
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






