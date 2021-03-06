﻿using UnityEngine;
using UnityEngine.Networking;

public class MoveTank : NetworkBehaviour
{

	public GameObject missilePrefab;
	public Transform endOfCanon;

	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}

		// Movement
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		// Firing cannon keys
		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFireMissile ();
		}
	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.green;
		// Make the Main Camera a child of the player object
		GameObject.Find ("Main Camera").transform.parent = gameObject.transform;
		// Move the camera to just above the player object
		GameObject.Find ("Main Camera").transform.localPosition = new Vector3 (0.0f, transform.position.y + 10, 0);
	}
	
	// Actually fire the cannon
	[Command]
	void CmdFireMissile ()
	{
		var missile = (GameObject)Instantiate (missilePrefab, endOfCanon.position, endOfCanon.rotation);
		missile.GetComponent<Rigidbody> ().velocity = missile.transform.forward * 6;
		NetworkServer.Spawn (missile);
		Destroy (missile, 2.0f);
	}
}
