using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Adjustables")] // ADDS TITLE IN INSPECTOR - ZERO EFFECTS ON CODE
    public float speed;
    public float jumpHeight;

    [Header("Ground Check")]
    public float groundDistance; // MAX DISTANCE FROM FEET TO GROUND
    public LayerMask groundLayer;

    // REFERENCES
    private Rigidbody2D rb;
    private GameObject feet;

    // OTHER
    private float horizInput;
    private Vector2 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GameObject.Find("Feet");
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpHeight;
        }
        //allow double jump?

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizInput, rb.velocity.y);
    }

    private bool isGrounded()
    {
        // CHECKS BELOW SPRITE AT THREE POINTS. INSTEAD OF CHECKING OVERLAPPING COLLIDERS, THIS IS MORE OF A MANUAL CHECK IN A STRAIGHT LINE, OVERLAPPING AT ANY SIZE WOULD ALLOW CLIMBING WALLS, IDK WHY BUT THIS FIXES IT
        // TO CHECK IF PLAYER IS GROUNDED, USE 'isGrounded()' AS YOU WOULD A NORMAL BOOL
        if (Physics2D.Raycast((Vector2)feet.transform.GetChild(0).transform.position, -transform.up, groundDistance, groundLayer)) return true; // LEFT
        if (Physics2D.Raycast((Vector2)feet.transform.position, -transform.up, groundDistance, groundLayer)) return true; // CENTER
        if (Physics2D.Raycast((Vector2)feet.transform.GetChild(1).transform.position, -transform.up, groundDistance, groundLayer)) return true; // RIGHT
        return false; // REUTRNS FALSE IF NOT GROUNDED
    }
}
