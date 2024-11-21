using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
	public float pushForce = 10f;
	private Rigidbody turnikeRigidbody;

	private void Start()
	{
		//turnikeRigidbody = GetComponentInParent<Rigidbody>();
	}

	private void OnCollisionStay(Collision collision)
	{
			Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
			if (playerRigidbody != null)
			{
				// Turnikenin dönüş yönünü al
				Vector3 turnikeDirection = Vector3.zero;

				if (turnikeRigidbody != null)
				{
					// Turnikenin açısal hızını kullanarak kuvvet yönü belirle
					turnikeDirection = Vector3.Cross(turnikeRigidbody.angularVelocity, transform.up).normalized;
				}
				else
				{
					// Eğer Rigidbody yoksa çubuğun kendi hareket yönünü temel al
					turnikeDirection = transform.right; // Çubuğun lokal yönü
				}

				// Oyuncuya kuvvet uygula
				playerRigidbody.AddForce(turnikeDirection * pushForce, ForceMode.VelocityChange);
			}
	}
}
