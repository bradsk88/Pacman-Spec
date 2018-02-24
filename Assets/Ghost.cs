using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    private List<Vector2> bestPath;

    public GameObject marker;

    private void Start()
    {
        var start = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        bestPath = findPacMan(start, new List<Vector2>(new []{start}), new Vector2[] {}, 0);
        Debug.Log("Best path: " + bestPath);
    }

    private void Update()
    {
        //for (var i = 0; i < bestPath.Count - 1; i++)
        //{
        //   Debug.DrawLine(bestPath[i], bestPath[i + 1]);
        //}
    }

    private List<Vector2> findPacMan(Vector2 fromHere, List<Vector2> allPoints, Vector2[] goneSoFar, int depth)
    {
        Debug.Log("Searching at " + fromHere + " DEPTH[" + depth + "]");
        var g = Instantiate(marker);
        g.transform.position = fromHere;

        if (depth > 20)
        {
            return new List<Vector2>(new[] {fromHere});
        }

        var bestLeft = go(fromHere, goneSoFar, -1, 0, depth + 1);
        var bestRight = go(fromHere, goneSoFar, 1, 0, depth + 1);
        var bestTop = go(fromHere, goneSoFar, 0, 1, depth + 1);
        var bestBot = go(fromHere, goneSoFar, 0, -1, depth + 1);

        var leftLength = getLength(bestLeft);
        var rightLength = getLength(bestRight);
        var topLength = getLength(bestTop);
        var botLength = getLength(bestBot);

        var shortestLength = Math.Min(leftLength, Math.Min(rightLength, Math.Min(topLength, botLength)));
        if (leftLength == shortestLength)
        {
            return bestLeft;
        }

        if (rightLength == shortestLength)
        {
            return bestRight;
        }

        if (topLength == shortestLength)
        {
            return bestTop;
        }

        return bestBot;
    }

    private List<Vector2> go(Vector2 fromHere, Vector2[] goneSoFar, int xOff, int yOff, int depth)
    {
        var leftGo = new Vector2(fromHere.x + xOff, fromHere.y + yOff);
        if (hitsPacMan(fromHere, leftGo))
        {
            Debug.Log("Hit pacman at " + leftGo);
            return new List<Vector2>(new[] {leftGo});
        }

        if (isOpen(goneSoFar, fromHere, leftGo))
        {
            var gsf = new Vector2[goneSoFar.Length + 1];
            goneSoFar.CopyTo(gsf, 0);
            gsf[gsf.Length - 1] = leftGo;
            return findPacMan(leftGo, gsf, depth + 1);
        }

        return null;
    }

    private int getLength(List<Vector2> trail)
    {
        if (trail == null)
        {
            return int.MaxValue;
        }

        return trail.Count;
    }

    private bool isOpen(Vector2[] goneSoFar, Vector2 initialV, Vector2 targetV)
    {
        if (Array.IndexOf(goneSoFar, targetV) >= 0)
        { // have already visited
            return false;
        };
        return !Physics2D.Linecast(initialV, targetV, 1 << LayerMask.NameToLayer("Walls"));
    }

    private bool hitsPacMan(Vector2 initialV, Vector2 targetV)
    {
        return Physics2D.Linecast(initialV, targetV, 1 << LayerMask.NameToLayer("Player"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ghost touched player happened");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}