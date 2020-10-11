using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // the SerializeField tag adds the field to the Inspector in Unity.  
    // Look at the script on the gameobject in Unity, and you'll be able to update these fields during runtime.
    [SerializeField]
    float speed;
    [SerializeField]
    bool useRigidBody = false;

    Rigidbody2D rb;

    void Start()
    {
        speed = 5f;

        // 'this.GetComponent' finds the indicated component on the object, so we can store it in a variable
        rb = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // "Input" is a built in unity class, and GetAxis is a member function of the Input class.
        // "Horizontal" and "Vertical" just correspond to the WASD/arrow keys
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // we can use the variables the user inputs to create a new 3 dimensional Vector object to indicate movement:
        Vector3 mvmt = new Vector3(x, y, 0);


        // There are a few ways to move an object...either update it's actual position, 
        // or use the RigidBody to simulate physics.


        if (useRigidBody)
        {
            // using physics:
            //Rigidbody and Rigidbody2D have member functions that simulate physics.  
            // we can use the mvmt vector to simulate a force being applied to the object:
            rb.AddForce(mvmt * speed * Time.deltaTime,ForceMode2D.Impulse);
            // just for fun, find the Rigidbody component of this object
            //in the Unity editor, and try changing the mass or the gravity scale on the object, and see what happens.
        }
        else
        {
            //updating position:
            // every object in the game has a Transform component, which defines it's location and position.
            // we just add the mvmt vector to it's current position to change it's location.
            this.transform.position += mvmt * speed * Time.deltaTime;

        }

        // when the user hits 'space' we can add a force in the positive Y direction to make the block jump.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 1, 0) * speed,ForceMode2D.Impulse);

            // Debug.Log() is a really useful function in general.  look at the console in Unity, and you'll see the debug output.
            Debug.Log("jump!");
        }
        
    }
}
