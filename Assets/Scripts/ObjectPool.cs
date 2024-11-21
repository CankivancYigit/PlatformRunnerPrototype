using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonBase<ObjectPool>
{
	[SerializeField] private GameObject prefab;
	[SerializeField] private int initialPoolSize = 20;
	[SerializeField] private Canvas canvas;

	private Queue<GameObject> _pool;

	private void Start()
	{
		if (canvas == null)
		{
			Debug.LogError("Canvas reference is not set in the ObjectPool!");
			return;
		}

		_pool = new Queue<GameObject>();

		for (int i = 0; i < initialPoolSize; i++)
		{
			CreateNewObject();
		}
	}

	private void CreateNewObject()
	{
		// Prefab'ı canvas altında oluştur
		GameObject obj = Instantiate(prefab, canvas.transform);
		obj.SetActive(false); // Aktif olmasın
		_pool.Enqueue(obj); // Havuz sırasına ekle
	}

	public GameObject GetObject()
	{
		if (_pool.Count == 0)
		{
			CreateNewObject(); // Havuz boşsa yeni nesne yarat
		}

		GameObject obj = _pool.Dequeue(); // Havuzdan bir nesne al
		obj.SetActive(true); // Aktif hale getir
		return obj;
	}

	public void ReturnObject(GameObject obj)
	{
		obj.SetActive(false);
		_pool.Enqueue(obj); // Havuz sırasına geri ekle
	}
}