using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	
	private const float Speed = 0.05f;
	private Vector2[] Directions = {Vector2.up, Vector2.left, Vector2.down, Vector2.right};
	private int _direction = 0; 

	private Vector2 _target;
	
	private bool _moving;
	
	private void Start()
	{
		_target = transform.position;
		_moving = true;
	}

	private void FixedUpdate()
	{
		if (!_moving)
		{
			ChooseNewTarget();
		}
	}

	private void ChooseNewTarget()
	{
		Debug.Log("Choosing a new target");
		Debug.Log("Current movement vector: " + _direction);

		Vector2 pos2d = new Vector2(transform.position.x, transform.position.y);
		var proceed = pos2d + Directions[_direction]; 
		if (!isWall(transform.position, proceed))
		{
			_target = proceed;
			_moving = true;
			return;
		}

		for (var i = 0; i < Directions.Length - 1; i++)
		{
			var newIndex = (_direction + i) % Directions.Length;
			var newTarget = pos2d + Directions[newIndex];
			if (!isWall(transform.position, newTarget))
			{
				_target = newTarget;
				_direction = newIndex;
				_moving = true;
				return;
			}
		}
	}


	private bool isWall(Vector2 initialV, Vector2 targetV)
	{
		return Physics2D.Linecast(initialV, targetV, 1 << LayerMask.NameToLayer("Walls"));
	}

	private void Update()
	{
		if (_moving)
		{
			Debug.DrawLine(transform.position, _target);
			transform.position = Vector2.MoveTowards(transform.position, _target, Speed);
			if (new Vector2(transform.position.x, transform.position.y).Equals(_target))
			{
				_moving = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Ghost touched player happened");
		if(other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
		}
	}

	public static Vector2 Left(Vector2 start)
	{
		return new Vector2(start.x - 1, start.y);
	}
	
	public static Vector2 Right(Vector2 start)
	{
		return new Vector2(start.x + 1, start.y);
	}
	
	public static Vector2 Up(Vector2 start)
	{
		return new Vector2(start.x, start.y + 1);
	}
	
	public static Vector2 Down(Vector2 start)
	{
		return new Vector2(start.x, start.y - 1);
	}
}
