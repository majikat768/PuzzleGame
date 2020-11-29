using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    GameObject energyIndicatorPanel;
    float movementDist = .5f;
    float moveSpeed = 4f;
    float rotSpeed = 25f;
    Vector3 pos; 

    [SerializeField]
    Energy.EnergyType energyType;

    void Start()
    {
        energyIndicatorPanel = GameObject.FindWithTag("EnergyIndicator");
        pos = transform.position;
        this.GetComponent<SpriteRenderer>().color = Energy.GetColor(this.energyType);
    }

    void Update()
    {
        pos += new Vector3(0,Mathf.Sin(moveSpeed * Time.fixedTime) * movementDist * Time.deltaTime,0);
        transform.position = pos;
        //transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetEnergyType(this.energyType);
            GameObject.FindWithTag("EnergyIndicator").GetComponentsInChildren<Image>()[1].color = this.gameObject.GetComponent<SpriteRenderer>().color;
            Destroy(this.gameObject);
        }
    }
}
