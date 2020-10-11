using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is to be attached to the Camera in the scene, so that the camera will follow the player.

public class CameraController : MonoBehaviour
{
    GameObject player;
    float offset;

    void Start()
    {
        // FindWithTag is a built in function that just locates the gameobject in the scene with the applied tag.
        // so now the variable player refers to the gameobject in the scene called Player.
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        // each frame,
        // find the player's position, and update the position of the camera.
        // we don't want to update the Z axis though.
        Vector3 playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y, this.transform.position.z);
        
    }
}
