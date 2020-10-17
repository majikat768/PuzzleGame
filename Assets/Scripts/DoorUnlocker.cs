using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {   
        //this checks for identical colors and then deletes the collider
        if (collision.gameObject.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color)
            GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //this puts the collider back on.
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
