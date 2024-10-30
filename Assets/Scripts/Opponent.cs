using System;
using UnityEngine;
public class Opponent : MonoBehaviour
{
	public float speed = 5f; 
	public Vector3 startPosition; 
	public LayerMask obstacleLayer; 
	public float avoidDistance = 1.5f;

	private void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		MoveForward();
		AvoidObstacles();
	}

	void MoveForward()
	{
		// Rakibi ileriye hareket ettir
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void AvoidObstacles()
	{
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position, transform.forward, out hit, avoidDistance, obstacleLayer))
		{
			Vector3 avoidDirection = Vector3.Cross(Vector3.up, hit.normal).normalized;
			transform.Translate(avoidDirection * speed * Time.deltaTime);
		}
	}

	// void OnCollisionEnter(Collision other)
	// {
	// 	
	// 	if (other.TryGetComponent(out ICollideable collideable) && !other.gameObject.CompareTag("RotatorStick"))
	// 	{
	// 		transform.position = startPosition;
	// 	}
	// }

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.TryGetComponent(out ICollideable collideable))
		{
			transform.position = startPosition;
		}
	}
}