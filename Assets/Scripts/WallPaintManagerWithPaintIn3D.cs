using PaintCore;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;
using UnityEngine.Serialization;

public class WallPaintManagerWithPaintIn3D : MonoBehaviour
{
	public CwPaintSphere cwPaintSphere;
	public CwChannelCounterText cwChannelCounterText;
	public Color color1 = Color.yellow; 
	public Color color2 = Color.red; 
	public Color color3 = Color.blue; 
	
	public Slider brushSizeSlider;
	public Button colorButton1;
	public Button colorButton2;
	public Button colorButton3;
	
	private void Start()
	{
		cwPaintSphere.Scale = new Vector3(brushSizeSlider.value,brushSizeSlider.value,brushSizeSlider.value);
		colorButton1.onClick.AddListener(() => SetPaintColor(color1));
		colorButton2.onClick.AddListener(() => SetPaintColor(color2));
		colorButton3.onClick.AddListener(() => SetPaintColor(color3));
	}

	private void SetPaintColor(Color color)
	{
		cwPaintSphere.Color = color;
	}
	
	public void UpdateBrushSize()
	{
		cwPaintSphere.Scale = new Vector3(brushSizeSlider.value,brushSizeSlider.value,brushSizeSlider.value);
	}
	
	private void Update()
	{
		UpdateBrushSize();
	}
	
	//
	// private void UpdatePercentageText()
	// {
	// 	float paintedArea = paintIn3D.GetPaintedArea(); 
	// 	float totalArea = paintIn3D.GetTotalArea(); 
	// 	float percentage = (paintedArea / totalArea) * 100; 
	// 	percentageText.text = $"{percentage:F2}% boyandı"; 
	// }
}

