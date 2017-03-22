using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; // We do need this, right??

public class Tank : NetworkBehaviour {

	public int shieldDammage = 0;

	public void GetHit()
	{
		// Only update on the server version of objects (Authoritative server)
		if (!isServer) {
			return;
		}

		shieldDammage += 10;
		if (shieldDammage >= 100) {
			shieldDammage = 0;
			RpcRespawn ();
		}
	}
		
	// Server will say "Hey, we need to respawn,"
	// but call the Remote Procedure Call(RPC) on the client.
	// So, the client makes the player object spawn.

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer) {
			transform.position = Vector3.zero;
		}
	}
}
