using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

	private float _speed = 10f;
	public GameObject groundCheck;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		float hor = Input.GetAxis("Horizontal");
		Vector2 sideCheck = new Vector2(transform.position.x + (Math.Sign(hor)*0.5f), transform.position.y);
		bool sideStop = Physics2D.Linecast(transform.position, sideCheck, 1 << LayerMask.NameToLayer("Walls"));

		float tx = 0f;
		// 
		float vert = Input.GetAxis("Vertical");
		Vector2 topCheck = new Vector2(transform.position.x, transform.position.y + (Math.Sign(vert)*0.5f));
		bool topStop = Physics2D.Linecast(transform.position, topCheck, 1 << LayerMask.NameToLayer("Walls"));
		float ty = 0f;
		groundCheck.transform.position = topCheck;
			
		if (!sideStop)
		{
			tx = hor * _speed;
			tx *= Time.deltaTime;
		}

		if (!topStop)
		{
			ty = vert * _speed;
			ty *= Time.deltaTime;
		}

		transform.Translate(tx, ty, 0);
	}

}
