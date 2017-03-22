using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Missile : NetworkBehaviour {
	public GameObject explosionPrefab;

	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var tank = hit.GetComponent<Tank> ();

		if (tank != null) {
			tank.GetHit ();
			CmdCreateExplosion ();
		}
		Destroy (gameObject);
	}

	void CmdCreateExplosion()
	{
		var explosion = (GameObject)Instantiate (
			                explosionPrefab,
			                transform.position,
			                transform.rotation);
		Destroy (explosion, 2.0f);
	}
}
