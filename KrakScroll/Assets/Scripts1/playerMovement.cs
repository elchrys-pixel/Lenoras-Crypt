using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float horizInput;
    private GameObject feet;
    private bool isGrounded;

    public float speed;
    public float jumpHeight;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GameObject.Find("feet");
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapBox(feet.transform.position, feet.GetComponent<BoxCollider2D>().size, 0, groundLayer);
        if (isGrounded & Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpHeight;
        }
        //cannot attach boxcollider2d to object as it won't move then. BUG will stay attached to the "ground" when mid jump
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizInput, rb.velocity.y);
    }
}
