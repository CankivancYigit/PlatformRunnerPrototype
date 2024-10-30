using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleStick : MonoBehaviour
{
	[SerializeField] private float pushForce = 5f;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.TryGetComponent(out Player player))
		{
			Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
			if (rb != null && !rb.isKinematic)
			{
				Vector3 pushDirection = (player.transform.position - transform.position).normalized;
				rb.AddForce(pushDirection * pushForce * Time.deltaTime);
			}
		}
	}

	// private void OnCollisionStay(Collision other)
	// {
	// 	if (other.gameObject.TryGetComponent(out Player player))
	// 	{
	// 		Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
	// 		if (rb != null && !rb.isKinematic)
	// 		{
	// 			Vector3 pushDirection = (player.transform.position - transform.position).normalized;
	// 			rb.AddForce(pushDirection * pushForce * Time.deltaTime);
	// 		}
	// 	}
	// }
}
