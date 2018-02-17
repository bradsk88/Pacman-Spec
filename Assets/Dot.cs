using UnityEngine;

public class Dot : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collision happened");
		if(other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
			Score._score += 100;
		}
	}
}
