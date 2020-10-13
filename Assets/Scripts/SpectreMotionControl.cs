using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreMotionControl : MonoBehaviour
{
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody2D rb;
    private int maxJumps = 2;       //trying to allow double jump
    private int jumpCount = 0;      //struggling to find a way to reset this.
                                    //i need some sort of reset on collision command.
    public int speedModifier = 1;
    public int jumpModifier = 1;

    // Start is called before the first frame update
    // Only called once, initial load settings only
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // Use for User input
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            jumpKeyPressed = true;

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called a set number of times per second irrelevant of framerate
    // Use for physics interactions
    void FixedUpdate()
    {
        //horizontal movement
        rb.velocity = new Vector3(horizontalInput * speedModifier, rb.velocity.y, 0);

        //jump
        if (jumpKeyPressed)
        {
            /*if (jumpCount <= maxJumps)        //this prevents jumps over the maximum
            {*/                                 //need a reset on jumpCount before implementing.
                Debug.Log("Jumping");
                ++jumpCount;
                rb.AddForce(Vector3.up * jumpModifier, ForceMode2D.Impulse);
            /*}
            else
                Debug.Log("Over max jumps.");*/
            jumpKeyPressed = false;
        }
    }
}
