using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DotSpawning : MonoBehaviour
{

	public GameObject _initialDot;
	
	// Use this for initialization
	void Start () {
		Debug.Log("Initial dot position: " + _initialDot.transform.position);
		spawn(new List<Vector2>(), _initialDot.transform.position, 0);
	}

	private void spawn(List<Vector2> spawnsSoFar, Vector2 dot, int depth)
	{
		if (depth > 100)
		{
			return;
		}
		
		Vector2 leftSpawn = new Vector2(dot.x - 1, dot.y);
		Vector2 rightSpawn = new Vector2(dot.x + 1, dot.y);
		Vector2 topSpawn = new Vector2(dot.x, dot.y + 1);
		Vector2 botSpawn = new Vector2(dot.x, dot.y - 1);
		Vector2[] spawns = {leftSpawn, rightSpawn, topSpawn, botSpawn};

		foreach (Vector2 s in spawns)
		{
			if (spawnsSoFar.Contains(s))
			{
				continue;
			}
			if (isGood(dot, s))
			{
				spawnsSoFar.Add(s);
				var x = Instantiate(_initialDot);
				x.transform.position = s;
				spawn(spawnsSoFar, x.transform.position, depth + 1);
			}
		}
	}

	private bool isGood(Vector2 initialV, Vector2 targetV)
	{
		return !Physics2D.Linecast(initialV, targetV, 1 << LayerMask.NameToLayer("Walls"));
	}
}
