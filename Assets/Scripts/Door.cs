using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Energy.EnergyType energyType;
    GameObject DoorSprite;

    void Start()
    {
        DoorSprite = transform.GetChild(0).gameObject;
        DoorSprite.GetComponent<SpriteRenderer>().color = Energy.GetColor(this.energyType);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().energyType.Equals(this.energyType))
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                MessageBox messageBox = GameObject.FindWithTag("MessageCanvas").GetComponentInChildren<MessageBox>();
                messageBox.SetText("You need the " + this.energyType.ToString() + " key");
                StartCoroutine(messageBox.ShowText());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().energyType.Equals(this.energyType))
            {
                StartCoroutine(CloseDoor());
            }
        }
        
    }

    private IEnumerator OpenDoor()
    {
        Color c = DoorSprite.GetComponent<SpriteRenderer>().color;
        while(c.a > 0f)
        {
            c.a -= Time.deltaTime;
            DoorSprite.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        c.a = 0f;
        DoorSprite.GetComponent<SpriteRenderer>().color = c;
        DoorSprite.GetComponent<BoxCollider2D>().enabled = false;
    }

    private IEnumerator CloseDoor()
    {
        Color c = DoorSprite.GetComponent<SpriteRenderer>().color;
        while(c.a < 1f)
        {
            c.a += Time.deltaTime;
            DoorSprite.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        c.a = 1f;
        DoorSprite.GetComponent<SpriteRenderer>().color = c;
        DoorSprite.GetComponent<BoxCollider2D>().enabled = true;
    }
}
