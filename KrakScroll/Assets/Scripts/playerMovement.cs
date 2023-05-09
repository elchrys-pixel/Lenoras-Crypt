using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Adjustables")] // ADDS TITLE IN INSPECTOR - ZERO EFFECTS ON CODE
    public float speed;
    public float jumpHeight;
    public float resetY;

    [Header("Ground Check")]
    public float groundDistance; // MAX DISTANCE FROM FEET TO GROUND
    public LayerMask groundLayer;

    // REFERENCES
    private Rigidbody2D rb;
    private GameObject feet;

    // OTHER
    private float horizInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GameObject.Find("Feet");
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        if ((CheckIsGrounded() || GetComponent<PlayerHealth>().isInAcid) && Input.GetButtonDown("Jump")) rb.velocity = Vector2.up * jumpHeight;
        //allow double jump?
        ResetOnMinY();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizInput, rb.velocity.y);
    }

    private bool CheckIsGrounded()
    {
        // CHECKS BELOW SPRITE AT THREE POINTS. INSTEAD OF CHECKING OVERLAPPING COLLIDERS, THIS IS MORE OF A MANUAL CHECK IN A STRAIGHT LINE, OVERLAPPING AT ANY SIZE WOULD ALLOW CLIMBING WALLS, IDK WHY BUT THIS FIXES IT
        // TO CHECK IF PLAYER IS GROUNDED, USE 'CheckIsGrounded()' AS YOU WOULD A NORMAL BOOL
        if (Physics2D.Raycast((Vector2)feet.transform.GetChild(0).transform.position, -transform.up, groundDistance, groundLayer)) return true; // LEFT
        if (Physics2D.Raycast((Vector2)feet.transform.position, -transform.up, groundDistance, groundLayer)) return true; // CENTER
        if (Physics2D.Raycast((Vector2)feet.transform.GetChild(1).transform.position, -transform.up, groundDistance, groundLayer)) return true; // RIGHT
        return false; // REUTRNS FALSE IF NOT GROUNDED
    }

    private void ResetOnMinY()
    {
        if (transform.position.y <= resetY) GameManager.ResetLevel();
    }
}
