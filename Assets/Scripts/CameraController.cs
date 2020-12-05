using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Camera cam;
    float offset;

    [SerializeField]
    float moveTime = 0.25f;

    Vector2 ViewportSize;
    Rect CurrentRect;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        ViewportSize = new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2);
        CurrentRect = new Rect(new Vector2(0, 0), ViewportSize);
        Debug.Log(ViewportSize);
        Debug.Log(CurrentRect);
        cam.transform.position = new Vector3(CurrentRect.x + CurrentRect.width / 2, CurrentRect.y + CurrentRect.height / 2, cam.transform.position.z);
        // FindWithTag is a built in function that just locates the gameobject in the scene with the applied tag.
        // so now the variable player refers to the gameobject in the scene called Player.
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        // new implementation:
            // check if player is within current camera viewport;
            // if not, move whole camera.
            // gives it a neat look & feel, makes the level feel bigger, or something
        Vector3 playerPos = player.transform.position;

        if(playerPos.x > CurrentRect.x + CurrentRect.width)
        {
            CurrentRect.x += CurrentRect.width;
            StartCoroutine(MoveToPlayer());
        }
        if(playerPos.x < CurrentRect.x)
        {
            CurrentRect.x -= CurrentRect.width;
            StartCoroutine(MoveToPlayer());
        }
        if(playerPos.y > CurrentRect.y + CurrentRect.height)
        {
            CurrentRect.y += CurrentRect.height;
            StartCoroutine(MoveToPlayer());
        }
        if(playerPos.y < CurrentRect.y)
        {
            CurrentRect.y -= CurrentRect.height;
            StartCoroutine(MoveToPlayer());
        }
        // each frame,
        // find the player's position, and update the position of the camera.
        // we don't want to update the Z axis though.
        //this.transform.position = new Vector3(playerPos.x, playerPos.y, this.transform.position.z);
        
    }

    IEnumerator MoveToPlayer()
    {
        Debug.Log("moving");
        float x = cam.transform.position.x;
        float y = cam.transform.position.y;
        Vector3 newPos = new Vector3(CurrentRect.x, CurrentRect.y, 0f) + 
                                    new Vector3(CurrentRect.width, CurrentRect.height, 0f) / 2 + 
                                    new Vector3(0, 0, cam.transform.position.z);
        float newX = newPos.x;
        float newY = newPos.y;
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(newX);
        Debug.Log(newY);
        while(x != newX || y != newY)
        {
            Debug.Log("moving");
            x = Mathf.MoveTowards(x, newX, moveTime);
            y = Mathf.MoveTowards(y, newY, moveTime);
            cam.transform.position = new Vector3(x, y, cam.transform.position.z);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
