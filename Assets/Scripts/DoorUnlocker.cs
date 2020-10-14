using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Knock knock!");

        //this checks for identical colors and then deletes the collider
        if (collision.gameObject.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color)
        {
            Destroy(GetComponent("BoxCollider2D"));
            Debug.Log("Door Unlocked");
        }
        else
            Debug.Log("Door locked");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("You forgot to lock the door.");

        //this puts the collider back on.
        //Currently this puts a collider back on AS SOON AS you disconnect, e.g. destroy collider
        //we need some kind of delay or check to see if the player has left the door
        //We can't get player location after the collider is gone. idk...
        BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
    }
}
