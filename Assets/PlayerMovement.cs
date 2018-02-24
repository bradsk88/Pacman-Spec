using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.XR.WSA;

public class PlayerMovement : MonoBehaviour
{
    private const float Speed = 0.1f;
    public GameObject groundCheck;
    public GameObject groundCheck2;
    private bool moving;
    private Vector2 _target;
    private Vector2 _target2;

    private bool dead;

    private void Start()
    {
        groundCheck2 = Instantiate(groundCheck);
        var material = groundCheck2.GetComponent<Renderer>().material;
        material.shader = Shader.Find("Specular");
        material.SetColor("_SpecColor", Color.red);
        _target = transform.position;
        _target2 = transform.position;
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            transform.Rotate(0,0,300*Time.deltaTime);
        }
        
        
        Debug.DrawLine(transform.position, _target);
        Debug.DrawLine(_target, _target2);
        groundCheck.transform.position = _target;
        groundCheck2.transform.position = _target2;
        
        transform.position = Vector2.MoveTowards(transform.position, _target, Speed);
        if (new Vector2(transform.position.x, transform.position.y).Equals(_target))
        {        
            if (_target.Equals(_target2))
            {
                moving = false;
                return;
            }
            

            
            bool stop = Physics2D.Linecast(_target, _target2, 1 << LayerMask.NameToLayer("Walls"));
            if (stop)
            {
                moving = false;
                return;
            }
            _target = _target2;
        }
    }

    private void Update()
    {
        if (dead)
        {
            return;
        }
        GetInput();
    }

    private void GetInput()
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        var htarget = new Vector2(_target.x + Math.Sign(hor), _target.y);
        var vtarget = new Vector2(_target.x, _target.y + Math.Sign(vert));

        if (Math.Abs(hor) > Math.Abs(vert)) // pushing more on L/R and U/D
        {
            _target2 = htarget;
        }
        else
        {
            _target2 = vtarget;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ghost")
        {
            dead = true;
            Destroy(other);
        }
    }
}