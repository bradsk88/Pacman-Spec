using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Ghost touched player happened");
		if(other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
		}
	}
}
