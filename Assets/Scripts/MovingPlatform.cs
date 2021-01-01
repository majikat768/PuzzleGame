using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;
    int direction = 1;
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    bool MoveWithPlayer = false;
    bool moving = false;

    private void Start()
    {
        transform.position = startPos; 
        if(!MoveWithPlayer)
        {
            moving = true;
        }
    }

    private void Update()
    {
        if (moving)
        {
            if (direction == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, endPos) < 0.1f)
                {
                    direction = -1;
                }
            }
            else if (!MoveWithPlayer && direction == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, startPos) < 0.1f)
                {
                    direction = 1;
                }
            }
        }
        else if (Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent = this.transform;
        if(MoveWithPlayer)
        {
            moving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.parent = null;
        if(MoveWithPlayer)
        {
            moving = false;
        }
    }
    /*
    GameObject player;
    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;
    [SerializeField]
    int speed = 3;
    [SerializeField]
    bool AlwaysMove = false;
    int direction = -1;
    bool onPlatform = false;
    [SerializeField]
    bool ReturnHome = true;

    void Start()
    {
        transform.position = startPos;
        player = GameObject.FindWithTag("Player");
        if (AlwaysMove)
            onPlatform = true;
    }

    void FixedUpdate()
    {
        if (onPlatform)
        {
            if (direction == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, endPos) < 0.1f) direction = -1;
            }
            if (direction == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, startPos) < 0.1f) direction = 1;
            }
        }
        else
        {
            if(transform.position != startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = this.transform;
        direction = 1;
        if(!AlwaysMove)
            onPlatform = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.parent = null;
        if (!AlwaysMove)
        {
            onPlatform = false;
            if (ReturnHome)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
                Debug.Log(direction);
            }
        }
    }
    */
}
